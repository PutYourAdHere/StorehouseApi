using System.Linq;
using System.Threading.Tasks;
using Domain.Crosscutting.MessageBroker;
using Microsoft.Extensions.Logging;
using Quartz;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;

namespace Storehouse.Application.Api.Jobs
{
    /// <summary>
    /// Job for searching products that has been expired. The expiration date is compared with today UTC time.
    /// </summary>
    public class ProductExpirationJob : IJob
    {
        /// <summary>
        /// Logging provider
        /// </summary>
        private readonly ILogger<ProductExpirationJob> _logger;

        /// <summary>
        /// Products repository for searching the expired items
        /// </summary>
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        /// Message broker for sending the notifications
        /// </summary>
        private readonly IMessageBroker<Product> _messageBroker;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">logging system</param>
        /// <param name="productsRepository">Repo for getting the expired products</param>
        /// <param name="messageBroker">notification system</param>
        public ProductExpirationJob(ILogger<ProductExpirationJob> logger, IProductsRepository productsRepository, IMessageBroker<Product> messageBroker)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _messageBroker = messageBroker;
        }

        /// <summary>
        /// Task execution. It searches the expired items and notify them
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("ProductExpirationJob execution started");
            
            var entities = await _productsRepository.GetExpiredProducts();

            if (entities != null && entities.Any())
            {
                _logger.LogInformation($"Some products has expired ({entities.Count})");
                _logger.LogDebug("{@entities}", entities);

                _messageBroker.Send(entities);
            }

            _logger.LogInformation("ProductExpirationJob execution finished");
        }
    }
}

using System;
using System.Threading.Tasks;
using Domain.Crosscutting.MessageBroker;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;
using Storehouse.Domain.Contracts.Services;

namespace Storehouse.Domain.Services
{
    /// <summary>
    /// Product Domain service. It manages how the products are created and updated
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Product repo
        /// </summary>
        private readonly IProductsRepository _productsRepository;

        /// <summary>
        /// Message broker for sending the notifications
        /// </summary>
        private readonly IMessageBroker<Product> _messageBroker;

        /// <summary>
        /// Default constructor. A Product Repository is mandatory
        /// </summary>
        /// <param name="productsRepository">The product repo for CRUD operations</param>
        /// /// <param name="messageBroker">The notification channel</param>
        public ProductService(IProductsRepository productsRepository, IMessageBroker<Product> messageBroker)
        {
            _productsRepository = productsRepository;
            _messageBroker = messageBroker;
        }

        /// <summary>
        /// Creates a new product in the database
        /// </summary>
        /// <param name="product">the product to be created</param>
        /// <returns>The new product created</returns>
        public Product Create(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            product.Id = Guid.Empty;
            _productsRepository.Insert(product);

            return product;
        }

        /// <summary>
        /// Updates only the stock of the product and its updated date. If the stock is lower than the current one, a notification is sent
        /// </summary>
        /// <param name="id">The id of the product to be updated</param>
        /// <param name="stock">the new stock</param>
        /// <returns>The updated product</returns>
        public async Task<Product> UpdateStock(Guid id, int stock)
        {
            if (stock < 0) throw new ArgumentNullException(nameof(stock));
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            var product = await _productsRepository.GetAsync(id);

            if (product == null) return null;

            product.Stock = stock;
            _productsRepository.Update(product);

            if (product.Stock > stock)
                _messageBroker.Send(product);

            return product;
        }
    }
}

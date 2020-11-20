
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Domain.Crosscutting.MessageBroker
{
    /// <summary>
    /// Message broker for sending notifications. It is a generic implementation. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageBroker<T> : IMessageBroker<T> where T : BaseEntity
    {
        /// <summary>
        /// Notification system host configuration settings
        /// </summary>
        private readonly IMqConfiguration _config;

        /// <summary>
        /// Loggin system
        /// </summary>
        private readonly ILogger<MessageBroker<T>> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config">The expected configuration</param>
        /// <param name="logger">The logging mechanism</param>
        public MessageBroker(IMqConfiguration config, ILogger<MessageBroker<T>> logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Sends one item
        /// </summary>
        /// <param name="item">the item to notify</param>
        public void Send(T item)
        {
            _logger.LogInformation("Item notification sent");
            _logger.LogInformation($"Topic: {item.GetType()} - Id: {item.Id}");
        }

        /// <summary>
        /// Notifies a set of items
        /// </summary>
        /// <param name="items">The list of items to notify</param>
        public void Send(List<T> items)
        {
            _logger.LogInformation("Item list notification sent");
            items.ForEach(x => _logger.LogInformation($"Topic: {x.GetType()} - Id: {x.Id}"));
            
        }
    }
}

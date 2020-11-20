using System;
using Domain.Crosscutting;

namespace Storehouse.Domain.Contracts.Model
{
    public class Product : BaseEntity
    {
        /// <summary>
        /// Expiration date of the product. It can be null if the product has not got expiration date
        /// </summary>
        public DateTime? ExpirationAt { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of product <see cref="ProductType"/>
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Number of items in stock
        /// </summary>
        public int Stock { get; set; }
    }
}

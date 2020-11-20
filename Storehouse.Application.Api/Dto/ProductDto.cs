using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Storehouse.Domain.Contracts.Model;


namespace Storehouse.Application.Api.Dto
{
    /// <summary>
    /// Product transfer object
    /// </summary>
    public class ProductDto 
    {
        /// <summary>
        /// Product unique identifier
        /// </summary>
        public Guid Id { get; set; }

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
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Number of items in stock
        /// </summary>
        public int Stock { get; set; }

    }
}

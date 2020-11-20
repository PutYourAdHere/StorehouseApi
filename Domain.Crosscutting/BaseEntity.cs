using System;

namespace Domain.Crosscutting
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Product unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Last time the product has been updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// When the product was created in the database
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}


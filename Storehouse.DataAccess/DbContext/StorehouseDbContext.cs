using Microsoft.EntityFrameworkCore;
using Storehouse.Domain.Contracts.Model;

namespace Storehouse.DataAccess.DbContext
{
    /// <summary>
    /// Specific DBContext for the storehouse api
    /// </summary>
    public class StorehouseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        /// <summary>
        /// Set of products entities
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options"></param>
        public StorehouseDbContext(DbContextOptions<StorehouseDbContext> options) : base(options)
        {}

        /// <summary>
        /// Defines how the model must be built
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsMapping());
        }
    }
}

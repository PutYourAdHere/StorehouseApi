using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storehouse.Domain.Contracts.Model;

namespace Storehouse.DataAccess.DbContext
{
    /// <summary>
    /// Defines the way to save the <see cref="Product"/> entity in the database
    /// </summary>
    public class ProductsMapping : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configuration method of the entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Products", "Storehouse")
                .HasIndex(i => i.Name).HasName("Products_NameIX");
            
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.Name).HasMaxLength(128).IsRequired();

            builder
                .Property(x => x.CreatedAt).IsRequired();

        }
    }
}

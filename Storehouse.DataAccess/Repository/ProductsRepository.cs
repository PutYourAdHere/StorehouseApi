using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;

namespace Storehouse.DataAccess.Repository
{
    /// <summary>
    /// Specific repository for <see cref="Product"/> entity
    /// </summary>
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        /// <summary>
        /// Default constructor that sets the <see cref="DbContext"/> of the repository
        /// </summary>
        /// <param name="dataContext"></param>
        public ProductsRepository(Microsoft.EntityFrameworkCore.DbContext dataContext) : base(dataContext)
        {}

        /// <summary>
        /// Returns the products that match (contains) the given name
        /// </summary>
        /// <param name="name">the word to search for</param>
        /// <returns>Returns all the matching products</returns>
        public Task<List<Product>> GetByName(string name)
        {
            return DataContext.Set<Product>().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        /// <summary>
        /// Returns the products that has been expired
        /// </summary>
        /// <returns>Returns all the expired products</returns>
        public Task<List<Product>> GetExpiredProducts()
        {
            return DataContext.Set<Product>().Where(x => x.ExpirationAt < DateTime.UtcNow).ToListAsync();
        }
    }
}

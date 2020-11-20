using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Crosscutting.Repository;
using Storehouse.Domain.Contracts.Model;

namespace Storehouse.Domain.Contracts.Repositories
{
    /// <summary>
    /// Product repository contract which defines the extended methods
    /// </summary>
    public interface IProductsRepository : IRepositoryWriter<Product>
    {
        /// <summary>
        /// Returns the products that match (contains) the given name
        /// </summary>
        /// <param name="name">the word to search for</param>
        /// <returns>Returns all the matching products</returns>
        Task<List<Product>> GetByName(string name);

        /// <summary>
        /// Returns the products that has been expired
        /// </summary>
        /// <returns>Returns all the expired products</returns>
        Task<List<Product>> GetExpiredProducts();
    }
}

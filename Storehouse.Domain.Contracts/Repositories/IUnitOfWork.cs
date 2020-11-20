using System.Threading.Tasks;

namespace Storehouse.Domain.Contracts.Repositories
{
    /// <summary>
    /// Ensures all transactions on different repositories are done in a single transaction
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository for operating (CRUD) products of the storehouse
        /// </summary>
        IProductsRepository Products { get; }

        /// <summary>
        /// Executes the transaction and throws an exception if objects are not persisted
        /// </summary>
        /// <returns>the number of entities created/updated/deleted in the database</returns>
        Task<int> SaveAsync();
    }
}

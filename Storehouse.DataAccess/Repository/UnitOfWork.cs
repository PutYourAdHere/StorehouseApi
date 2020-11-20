using System.Threading.Tasks;
using Storehouse.Domain.Contracts.Repositories;

namespace Storehouse.DataAccess.Repository
{
    /// <summary>
    /// Ensures all transactions on different repositories are done in a single transaction
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// The data context of the application 
        /// </summary>
        protected Microsoft.EntityFrameworkCore.DbContext DataContext { get; }

        /// <summary>
        /// Repository for operating (CRUD) products of the storehouse
        /// </summary>
        public IProductsRepository Products { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataContext">The data context to be provided to the repositories</param>
        public UnitOfWork(Microsoft.EntityFrameworkCore.DbContext dataContext)
        {
            DataContext = dataContext;
            Products = new ProductsRepository(DataContext);
        }

        /// <summary>
        /// Executes the transaction and throws an exception if objects are not persisted
        /// </summary>
        /// <returns>the number of entities created/updated/deleted in the database</returns>
        Task<int> IUnitOfWork.SaveAsync() => DataContext.SaveChangesAsync();
    }
}

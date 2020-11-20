using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Crosscutting;
using Domain.Crosscutting.Repository;
using Microsoft.EntityFrameworkCore;

namespace Storehouse.DataAccess.Repository
{
    /// <summary>
    /// Base repository which implements the usual CRUD functions
    /// </summary>
    /// <typeparam name="T">an entity which inherits from BaseEntity</typeparam>
    public class Repository<T> : IRepositoryWriter<T> where T : BaseEntity
    {
        /// <summary>
        /// Data context of the repository
        /// </summary>
        protected Microsoft.EntityFrameworkCore.DbContext DataContext { get; }

        /// <summary>
        /// Default constructor that sets the data context
        /// </summary>
        /// <param name="dataContext"></param>
        protected Repository(Microsoft.EntityFrameworkCore.DbContext dataContext) => DataContext = dataContext;

        /// <summary>
        /// Search an entity by its Id
        /// </summary>
        /// <param name="id">The Id to search for</param>
        /// <returns>If any, the searched entity</returns>
        public ValueTask<T> GetAsync(Guid id) => DataContext.Set<T>().FindAsync(id);

        /// <summary>
        /// Returns all the entities of the repository
        /// </summary>
        /// <returns>All the entities</returns>
        public Task<List<T>> GetAsync() => DataContext.Set<T>().ToListAsync();

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">The entity to be created. Id must not be set</param>
        public void Insert(T entity)
        {
            entity.Id = Guid.Empty;
            entity.CreatedAt = DateTime.UtcNow;

            DataContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Updates a given entity
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Disposes the datacontext of the repository
        /// </summary>
        public void Dispose() => DataContext?.Dispose();
    }
}

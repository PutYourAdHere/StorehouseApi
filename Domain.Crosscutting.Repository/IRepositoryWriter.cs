using System;

namespace Domain.Crosscutting.Repository
{
    /// <summary>
    /// Defines the reader methods for a repository with readonly and write access
    /// </summary>
    /// <typeparam name="T">Any entity which inherits from <see cref="BaseEntity"/></typeparam>
    public interface IRepositoryWriter<T> : IRepositoryReader<T>, IDisposable where T : BaseEntity
    {
        /// <summary>
        /// Creates a new element 
        /// </summary>
        /// <param name="entity">The new item to be created</param>
        void Insert(T entity);
        
        /// <summary>
        /// Updates an existing element
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        void Update(T entity);
    }
}

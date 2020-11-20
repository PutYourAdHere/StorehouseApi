using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Crosscutting.Repository
{
    /// <summary>
    /// Defines the reader methods for a repository with readonly access
    /// </summary>
    /// <typeparam name="T">Any entity which inherits from <see cref="BaseEntity"/></typeparam>
    public interface IRepositoryReader<T> : IDisposable where T : BaseEntity
    {
        /// <summary>
        /// Returns an element by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<T> GetAsync(Guid id);

        /// <summary>
        /// Returns all the elements
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAsync();
    }
}

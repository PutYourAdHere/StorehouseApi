using System;
using System.Threading.Tasks;
using Storehouse.Domain.Contracts.Model;

namespace Storehouse.Domain.Contracts.Services
{
    public interface IProductService
    {
        Product Create(Product product);

        Task<Product> UpdateStock(Guid id, int stock);
    }
}

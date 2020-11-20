using Storehouse.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using Moq;
using Storehouse.Domain.Contracts.Model;
using Domain.Crosscutting.MessageBroker;

namespace Storehouse.Domain.Test.UsingProductService
{
    public abstract class UsingProductService : IDisposable
    {
        protected readonly Mock<IProductsRepository> ProductsRepository;
        protected readonly Mock<IMessageBroker<Product>> MessageBroker;

        protected readonly List<Product> Products;

        protected UsingProductService()
        {
            ProductsRepository = new Mock<IProductsRepository>();
            MessageBroker = new Mock<IMessageBroker<Product>>();

            Products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    ExpirationAt = DateTime.Now.AddDays(20),
                    Name = "Torreznos",
                    ProductType = ProductType.Food,
                    Stock = 11,
                    UpdatedAt = null
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    ExpirationAt = DateTime.Now.AddDays(-20),
                    Name = "Cocacola",
                    ProductType = ProductType.Drink,
                    Stock = 10,
                    UpdatedAt = null
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    ExpirationAt = null,
                    Name = "Lejia",
                    ProductType = ProductType.Cleaning,
                    Stock = 2,
                    UpdatedAt = null
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    ExpirationAt = null,
                    Name = "Estropajo",
                    ProductType = ProductType.Cleaning,
                    Stock = 5,
                    UpdatedAt = null
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    ExpirationAt = DateTime.Now.AddDays(5),
                    Name = "Chorizo iberico",
                    ProductType = ProductType.Food,
                    Stock = 12,
                    UpdatedAt = null
                }
            };
        }


        public abstract void Dispose();
    }
}

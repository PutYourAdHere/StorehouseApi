using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using Storehouse.Application.Api.Dto;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;
using Storehouse.Domain.Contracts.Services;

namespace Storehouse.Application.Api.Test.UsingProductController
{
    public abstract class UsingProductController : IDisposable
    {
        protected readonly Mock<IMapper> Mapper;
        protected readonly Mock<IUnitOfWork> UnitOfWork;
        protected readonly Mock<IProductService> ProductService;
        protected readonly Mock<IProductsRepository> ProductsRepository;
         
        protected readonly List<Product> Products;
        protected List<ProductDto> ProductsDto;

        protected UsingProductController()
        {
            Mapper = new Mock<IMapper>();
            UnitOfWork = new Mock<IUnitOfWork>();
            ProductService = new Mock<IProductService>();
            ProductsRepository = new Mock<IProductsRepository>();
            UnitOfWork.Setup(mock => mock.Products).Returns(ProductsRepository.Object);

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

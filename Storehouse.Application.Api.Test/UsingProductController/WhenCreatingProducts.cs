using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Storehouse.Application.Api.Controllers;
using Storehouse.Application.Api.Dto;
using Storehouse.Domain.Contracts.Model;
using Xunit;

namespace Storehouse.Application.Api.Test.UsingProductController
{

    public class WhenCreatingProducts : UsingProductController
    {

        private readonly ProductsController _productController;

        public WhenCreatingProducts()
        {
            _productController = new ProductsController(ProductService.Object, UnitOfWork.Object, Mapper.Object);
        }

        [Fact]
        public async void ItShouldCreateProduct()
        {
            var productDto = new ProductDto
            {
                Name = "fakeProduct",
                ExpirationAt = DateTime.Now,
                ProductType = ProductType.Food,
                Stock = 5,
                Id = Guid.Empty
            };

            var product = new Product
            {
                CreatedAt = DateTime.Now,
                Name = productDto.Name,
                ExpirationAt = DateTime.Now,
                ProductType = productDto.ProductType,
                Stock = productDto.Stock,
                Id = Guid.NewGuid()
            };

            ProductService.Setup(mock => mock.Create(product)).Returns(product);
            Mapper.Setup(mock => mock.Map<Product>(productDto)).Returns(product);
            Mapper.Setup(mock => mock.Map<ProductDto>(product)).Returns(new ProductDto
            {
                Name = product.Name,
                ExpirationAt = product.ExpirationAt,
                ProductType = product.ProductType,
                Stock = product.Stock,
                Id = product.Id
            });

            var result = (await _productController.Post(productDto)).Result as OkObjectResult;

            var entity = result?.Value as ProductDto;

            ProductService.Verify(x => x.Create(product), Times.Once);
            UnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);

            Assert.NotNull(entity);
            Assert.True(entity.Id != Guid.Empty);

        }

        [Fact]
        public async void ItShouldReturnBadRequestWhenNullProduct()
        {
            ProductService.Setup(mock => mock.Create(null)).Throws<ArgumentNullException>();
            Mapper.Setup(mock => mock.Map<Product>(null)).Returns(() => null);

            var result = (await _productController.Post(null)).Result as BadRequestObjectResult;

            ProductService.Verify(x => x.Create(null), Times.Once);
            UnitOfWork.Verify(x => x.SaveAsync(), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(result.StatusCode == 400);
        }

        public override void Dispose()
        {}
    }
}

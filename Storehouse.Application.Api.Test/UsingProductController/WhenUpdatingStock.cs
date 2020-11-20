using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Storehouse.Application.Api.Controllers;
using Storehouse.Application.Api.Dto;
using Storehouse.Domain.Contracts.Model;
using Xunit;

namespace Storehouse.Application.Api.Test.UsingProductController
{

    public class WhenUpdatingStock : UsingProductController
    {

        private readonly ProductsController _productController;

        public WhenUpdatingStock()
        {
            _productController = new ProductsController(ProductService.Object, UnitOfWork.Object, Mapper.Object);
        }

        [Fact]
        public async void ItShouldUpdateStock()
        {
            var stockDto = new StockChangeDto()
            {
                Stock = 5,
            };

            var product = Products.FirstOrDefault();

            var updatedProduct = product;
            updatedProduct!.Stock = stockDto.Stock;
            var updatedDto = new ProductDto
            {
                Name = updatedProduct.Name,
                Stock = updatedProduct.Stock,
                ProductType = updatedProduct.ProductType,
                ExpirationAt = updatedProduct.ExpirationAt,
                Id = updatedProduct.Id
            };


            ProductService.Setup(mock => mock.UpdateStock(product!.Id, stockDto.Stock)).Returns(Task.Run(() => updatedProduct));
            Mapper.Setup(mock => mock.Map<ProductDto>(product)).Returns(updatedDto);

            var result = (await _productController.Put(product!.Id, stockDto)).Result as OkObjectResult;

            var entity = result?.Value as ProductDto;

            ProductService.Verify(x => x.UpdateStock(product.Id, stockDto.Stock), Times.Once);
            UnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);

            Assert.NotNull(entity);
            Assert.True(entity.Stock == stockDto.Stock);
        }

        [Fact]
        public async void ItShouldReturnBadRequestWhenThrowArgumentNullException()
        {
            var stockDto = new StockChangeDto()
            {
                Stock = -5,
            };

            var product = Products.FirstOrDefault();

            var updatedProduct = product;
            updatedProduct!.Stock = stockDto.Stock;
            var updatedDto = new ProductDto
            {
                Name = updatedProduct.Name,
                Stock = updatedProduct.Stock,
                ProductType = updatedProduct.ProductType,
                ExpirationAt = updatedProduct.ExpirationAt,
                Id = updatedProduct.Id
            };


            ProductService.Setup(mock => mock.UpdateStock(product!.Id, stockDto.Stock)).Throws<ArgumentNullException>();

            var result = (await _productController.Put(product!.Id, stockDto)).Result as BadRequestObjectResult;

            ProductService.Verify(x => x.UpdateStock(product.Id, stockDto.Stock), Times.Once);
            UnitOfWork.Verify(x => x.SaveAsync(), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.True(result.StatusCode == 400);

        }

        [Fact]
        public async void ItShouldReturnNotFoundWhenIdIsWrong()
        {
            var stockDto = new StockChangeDto
            {
                Stock = 5,
            };

            var product = Products.FirstOrDefault();

            ProductService.Setup(mock => mock.UpdateStock(product!.Id, stockDto.Stock)).Returns(Task.FromResult<Product>(null));

            var result = (await _productController.Put(product!.Id, stockDto)).Result as NotFoundResult;

            ProductService.Verify(x => x.UpdateStock(product.Id, stockDto.Stock), Times.Once);
            UnitOfWork.Verify(x => x.SaveAsync(), Times.Never);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            Assert.True(result.StatusCode == 404);
        }

        public override void Dispose()
        {}

    }
}

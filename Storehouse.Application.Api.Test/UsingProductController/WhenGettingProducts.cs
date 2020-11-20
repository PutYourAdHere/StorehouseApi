using System.Collections.Generic;
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

    public class WhenGettingProducts : UsingProductController
    {

        private readonly ProductsController _productController;

        public WhenGettingProducts() 
        {
            _productController = new ProductsController(ProductService.Object, UnitOfWork.Object, Mapper.Object);
            ProductsRepository.Setup(mock => mock.GetAsync()).Returns(Task.Run(() => Products));
        }

        [Fact]
        public async void ItShouldGetAllTheProducts()
        {
            
            ProductsRepository.Setup(mock => mock.GetByName(null)).Returns(Task.Run(() => new List<Product>()));
            ProductsDto = new List<ProductDto>();
            Products.ForEach(x => ProductsDto.Add(new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                ExpirationAt = x.ExpirationAt,
                Stock = x.Stock,
                ProductType = x.ProductType
            }));
            Mapper.Setup(mock => mock.Map<List<ProductDto>>(Products)).Returns(ProductsDto);

            

            var result = (await _productController.Get(null)).Result as OkObjectResult;

            var entities = result?.Value as IEnumerable<ProductDto>;
            var productDtos = entities?.ToList();


            ProductsRepository.Verify(x => x.GetByName(null), Times.Never);
            ProductsRepository.Verify(x => x.GetAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);

            Assert.NotNull(entities);
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(entities);

            Assert.True(productDtos.Any());
            Assert.True(productDtos.Count() == ProductsDto.Count());

        }

        [Theory]
        [InlineData("torr")]
        [InlineData("TORR")]
        [InlineData("OrR")]
        public async void ItShouldGetProductsMatchingByName(string name)
        {
            var foundProducts = Products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            ProductsRepository.Setup(mock => mock.GetByName(name)).Returns(Task.Run(() => foundProducts));
            ProductsDto = new List<ProductDto>();
            foundProducts.ForEach(x => ProductsDto.Add(new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                ExpirationAt = x.ExpirationAt,
                Stock = x.Stock,
                ProductType = x.ProductType
            }));
            Mapper.Setup(mock => mock.Map<List<ProductDto>>(foundProducts)).Returns(ProductsDto);

            var result = (await _productController.Get(name)).Result as OkObjectResult;

            var entities = result?.Value as IEnumerable<ProductDto>;
            var productDtos = entities?.ToList();


            ProductsRepository.Verify(x => x.GetByName(name), Times.Once);
            ProductsRepository.Verify(x => x.GetAsync(), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);

            Assert.NotNull(entities);
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(entities);

            Assert.True(productDtos.Any());
            Assert.True(productDtos.Count() == ProductsDto.Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void ItShouldReturnNotFoundWhenNameIsNullOrEmpty(string name)
        {
            ProductsRepository.Setup(mock => mock.GetByName(null)).Returns(Task.Run(() => new List<Product>()));
            ProductsDto = new List<ProductDto>();
            Products.ForEach(x => ProductsDto.Add(new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                ExpirationAt = x.ExpirationAt,
                Stock = x.Stock,
                ProductType = x.ProductType
            }));
            Mapper.Setup(mock => mock.Map<List<ProductDto>>(Products)).Returns(ProductsDto);

            var result = (await _productController.Get(name)).Result as OkObjectResult;

            var entities = result?.Value as IEnumerable<ProductDto>;
            var productDtos = entities?.ToList();

            ProductsRepository.Verify(x => x.GetByName(name), Times.Never);
            ProductsRepository.Verify(x => x.GetAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result.StatusCode == 200);

            Assert.NotNull(entities);
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(entities);
            Assert.True(productDtos.Any());
        }

        public override void Dispose()
        { }
    }
}

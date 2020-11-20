using Storehouse.Domain.Services;
using Xunit;

namespace Storehouse.Domain.Test.UsingProductService
{
    public class WhenUpdatingStock : UsingProductService
    {
        private readonly ProductService _productService;

        public WhenUpdatingStock()
        {
            _productService = new ProductService(ProductsRepository.Object, MessageBroker.Object);
        }

        [Fact]
        public void ItShouldUpdateStockAndSendNotification()
        {

        }

        [Fact]
        public void ItShouldUpdateStockAndNotSendNotification()
        {

        }

        [Fact]
        public void ItShouldThrowExceptionWhenStockNegative()
        {

        }

        [Fact]
        public void ItShouldThrowExceptionWhenIdEmpty()
        {

        }

        [Fact]
        public void ItShouldReturnNullWhenIdNotFound()
        {

        }

        public override void Dispose()
        { }
    }
}

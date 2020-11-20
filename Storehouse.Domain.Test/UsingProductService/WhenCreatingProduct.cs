using Storehouse.Domain.Services;
using Xunit;

namespace Storehouse.Domain.Test.UsingProductService
{
    public class WhenCreatingProduct : UsingProductService
    {
        private readonly ProductService _productService;

        public WhenCreatingProduct()
        {
            _productService = new ProductService(ProductsRepository.Object, MessageBroker.Object);
        }

        [Fact]
        public void ItShouldCreateProduct()
        {

        }

        [Fact]
        public void ItShouldThrowExceptionWhenProductNull()
        {

        }


        public override void Dispose()
        { }
    }
}

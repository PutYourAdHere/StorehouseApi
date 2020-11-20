
namespace Storehouse.Application.Api.Dto
{
    /// <summary>
    /// Dto for sending the stock value of a product
    /// </summary>
    public class StockChangeDto 
    {
        /// <summary>
        /// The stock value of a product
        /// </summary>
        public int Stock { get; set; }
    }
}

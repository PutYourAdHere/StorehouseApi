using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storehouse.Application.Api.Dto;
using Storehouse.Domain.Contracts.Model;
using Storehouse.Domain.Contracts.Repositories;
using Storehouse.Domain.Contracts.Services;

namespace Storehouse.Application.Api.Controllers
{
    /// <summary>
    /// Product stock API. Allows to get the list of available products and their stock, update the stock and search by name
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Entity mapper for Application Layer and Domain Layer
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Unit of work for transactional execution of the repositories 
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Unit of work for transactional execution of the repositories 
        /// </summary>
        protected IProductService ProductService { get; }

        /// <summary>
        /// Default constructor. Initializes with a unit of work and the entity mapper
        /// </summary>
        /// /// <param name="productService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public ProductsController(IProductService productService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            ProductService = productService;
        }

        /// <summary>
        /// Allows to get all the products or to search by name. In case name is null or empty, it will return all the products
        /// </summary>
        /// <param name="name">Name to search for</param>
        /// <returns>The list of products in the store. In case a name is given, only the ones that contains the name</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] string name)
        {
            var entities = string.IsNullOrEmpty(name) ? UnitOfWork.Products.GetAsync() : UnitOfWork.Products.GetByName(name);

            return Ok(Mapper.Map<List<ProductDto>>(await entities));
        }


        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productDto">the product to be created</param>
        /// <returns>The new created product</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> Post(ProductDto productDto)
        {
            try
            {
                var entity = Mapper.Map<Product>(productDto);

                var newProduct = ProductService.Create(entity);
                await UnitOfWork.SaveAsync();

                return Ok(Mapper.Map<ProductDto>(newProduct));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Changes only the stock value of a product
        /// </summary>
        /// <param name="id">The identifier of the product to be changed</param>
        /// <param name="stockChangeDto">The new stock value</param>
        /// <returns>The update product or Not found if it does not exist</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Put(Guid id, StockChangeDto stockChangeDto)
        {
            try
            {
                var entity = await ProductService.UpdateStock(id, stockChangeDto.Stock);

                if (entity != null)
                {
                    await UnitOfWork.SaveAsync();

                    return Ok(Mapper.Map<ProductDto>(entity));
                }

                return NotFound();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Core.Entities;
using E_Commerce.HandleResponses;
using E_Commerce.Helper;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Services.Helper;
using Services.Services.ProductService;
using Services.Services.ProductService.Dto;

namespace E_Commerce.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAll")]
        [Cache(10)]
        public async Task<ActionResult<Pagination<ProductResultDto>>> GetProducts([FromQuery]ProductSpecifications specifications)
        {
            var products = await _productService.GetProductsAsync(specifications);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [Cache(100)]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int? id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet]
        [Route("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
            => Ok(await _productService.GetProductBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
            => Ok(await _productService.GetProductTypesAsync());
    }
}

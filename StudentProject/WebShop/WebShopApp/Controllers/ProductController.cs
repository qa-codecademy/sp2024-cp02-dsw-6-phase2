using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.DTOs.Product;
using WebShopApp.Services.Interface;

namespace WebShopApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto addProductDto)
        {
            try
            {

                await _productService.AddProductAsync(addProductDto);
                return StatusCode(StatusCodes.Status201Created, "Product created");



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<ProductDto>> GetProducts()
        {
            try
            {

                return Ok(_productService.GetProducts());



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            try
            {

                var product = _productService.GetProductById(id);
                return Ok(product);



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {

                _productService.DeleteProduct(id);
                return Ok("Product deleted");




            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {

                await _productService.UpdateProduct(updateProductDto);
                return Ok("Product Updated");




            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [AllowAnonymous]
        [HttpGet("Filter")]
        public IActionResult Filter(int? category, string? brand)
        {
            try
            {

                var products = _productService.FilterProducts(category , brand );
                return Ok(products);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}

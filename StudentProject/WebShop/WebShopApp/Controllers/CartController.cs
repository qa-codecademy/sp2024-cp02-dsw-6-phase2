using Microsoft.AspNetCore.Mvc;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Cart;
using WebShopApp.Services.Interface;

namespace WebShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetUserCart(int userId)
        {
            try
            {

                var cartFromUser = await _cartServices.GetUserCart(userId);
                return Ok(cartFromUser);



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int userId, int productId, int quantity)
        {
            try
            {
                 await _cartServices.AddProductToCart(userId, productId, quantity);
                return Ok("Product added");



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpDelete]
        public async Task<ActionResult> DeleteProductFromCart(int userId, int productId)
        {
            try
            {
                await _cartServices.RemoveProductFromCart(userId, productId);
                return Ok("Product deleted");



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpDelete("clearCart")]
        public async Task <ActionResult> ClearCart(int userId)
        {
            try
            {
                await _cartServices.ClearCart(userId);
                return Ok("Cart cleared");



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }







    }
}

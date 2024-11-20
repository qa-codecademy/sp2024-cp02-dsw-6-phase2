using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Cart;
using WebShopApp.Mappers;
using WebShopApp.Services.Interface;

namespace WebShopApp.Controllers
{
    [Authorize]

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
        public async Task<ActionResult<Cart>> GetUserCart( int userId)
        {
            try
            {

                var cartFromUser = await _cartServices.GetUserCart(userId);
                var cart= cartFromUser.ToCart();
                return Ok(cart);



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromQuery] int userId, int productId, int quantity)
        {
            try
            {
                 await _cartServices.AddProductToCart(userId, productId, quantity);
                return Ok(new { message = "Product added to cart successfully." });



            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpDelete]
        public async Task<ActionResult> DeleteProductFromCart( int userId, int productId)
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

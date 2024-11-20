using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.OrderDtos;
using WebShopApp.Mappers;
using WebShopApp.Services.Implementation;
using WebShopApp.Services.Interface;
using XAct.Security;

namespace WebShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICartServices _cartService;
        private readonly MailjetService _mailjetService;
        public OrderController(IOrderService orderService, IUserService userService, IProductService productService, ICartServices cartServices, MailjetService mailjetService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
            _cartService = cartServices;
            _mailjetService = mailjetService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderDto addOrderDto)
        {
            // Extract the user ID from the token claims
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
            {
                return Unauthorized(); // Handle case where the user ID is not found
            }

            // Convert the userId to an integer
            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid user ID"); // Handle invalid user ID format
            }

           await _orderService.CreateOrder(userId , addOrderDto);   

            return Ok("Order created");

        }




        [HttpPost("AddProductToOrder")]
        public async Task<IActionResult> AddProductToOrder(int productId, int quantity)
        {
            try
            {
                

                // Extract the user ID from the token claims
                var userIdString = User.FindFirst("userId")?.Value;

                if (userIdString == null)
                {
                    return Unauthorized(); // Handle case where the user ID is not found
                }

                // Convert the userId to an integer
                if (!int.TryParse(userIdString, out int userId))
                {
                    return BadRequest("Invalid user ID"); // Handle invalid user ID format
                }

                // Fetch the user's order
                var order =  _orderService.GetOrderById(userId); // Get order based on userId
                if (order == null)
                {
                    order = new Orderr();
                    var orderDto = order.ToAddOrderDto();
                    await _orderService.CreateOrder(userId, orderDto);
                }

                // Fetch the existing product by ID
                var existingProduct =  _productService.GetProductById(productId);
                if (existingProduct == null)
                {
                    return NotFound("Product not found.");
                }

                // Check if the product already exists in the order (optional)
                var existingOrderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == existingProduct.Id);
                if (existingOrderItem != null)
                {
                    // Update the quantity if the product is already in the order
                    existingOrderItem.Quantity += quantity;
                }
                else
                {
                    // Add new OrderItem to the order
                    var newOrderItem = new OrderItem
                    {
                        ProductId = existingProduct.Id,
                        Quantity = quantity,
                        Product = existingProduct
                        
                    };
                    order.OrderItems.Add(newOrderItem);
                }

                // Save the order changes
                await _orderService.UpdateOrder(order);

                return Ok("Product added to order successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        [HttpGet]
        public async Task<IActionResult>GetAllOrders()
        {
            try
            {

                var order =  _orderService.GetAllOrders();
                return Ok(order);




            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpGet("GetOrders")]
        public IActionResult GetOrders(bool isOrderForUser)
        {
            int? userId = null;

            if (isOrderForUser)
            {
                // Extract the user ID from the claims using "userId" key
                var userIdClaim = User.FindFirst("userId");
                if (userIdClaim != null)
                {
                    userId = int.Parse(userIdClaim.Value);
                }
            }

            var orders = _orderService.GetOrders(isOrderForUser, userId);
            return Ok(orders);
        }







        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {

                var order = _orderService.GetOrderById(id);
                return Ok(order);




            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }



        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            try
            {

                var order = _orderService.DeleteOrderById(id);  
                return Ok(order);




            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [HttpPost("CeateOrderFromCart")]
        public async Task<IActionResult> CreateOrderFromCart(int cartId)
        {
            try
            {

                var order = await _orderService.CreateOrderFromCart(cartId); // Await the call
                Orderr orderOrig = OrderMapper.ToOrderOrig(order);
            
                return Ok(new { success = true, orderId = orderOrig.Id });


            }
            catch (DbUpdateException dbEx)
            {
                // Log or inspect the inner exception
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                return BadRequest(new { errors = new[] { "Failed to create order from cart" } });
              //  return StatusCode(StatusCodes.Status500InternalServerError, $"Database Update Error: {innerException}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                

            }
        }



    }
}

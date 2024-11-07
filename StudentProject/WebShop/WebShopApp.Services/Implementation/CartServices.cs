using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Cart;
using WebShopApp.Mappers;
using WebShopApp.Services.Interface;

namespace WebShopApp.Services.Implementation
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        public CartServices(ICartRepository cartRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public async Task AddProductToCart(int userId, int productId, int quantity)
        {
            // Get the user and their cart
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var cart = user.Cart;

            if (cart == null)
            {
                cart = new Cart { UserId = userId, User = user, CartItems = new List<CartItem>(), TotalAmount = 0 };
                await _cartRepository.Add(cart);
            }

            // Add product to cart
            await _cartRepository.AddProductToCartAsync(cart.Id, productId, quantity);
        }

        public async Task ClearCart(int userId)
        {
            var user =  _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var cart = _cartRepository.GetById(user.Cart.Id);
            if (cart == null)
            {
                throw new Exception("Cart not found for the user.");
            }

           await _cartRepository.ClearCartAsync(cart.Id);
        }

        public void DeleteCart(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var cart = _cartRepository.GetById(user.Cart.Id);
            if (cart == null)
            {
                throw new Exception("Cart not found for the user.");
            }
            _cartRepository.Delete(cart);

        }

        public List<CartDto> GetAllCarts()
        {
            var carts = _cartRepository.GetAll();
            return carts.Select(cart => cart.ToCartDto()).ToList();
        }

        public async Task<Cart> GetById(int cartId)
        {
          var cart = await _cartRepository.GetCartByIdAsync(cartId);
            return cart;

        }

        public async Task<CartDto> GetUserCart(int userId)
        {
            var user =  _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var cart =   user.Cart;
            if (cart == null)
            {
                // Automatically create a new cart for the user if it doesn't exist
                cart = new Cart { UserId = userId,User=user, CartItems = new List<CartItem>(), TotalAmount = 0 };
                await _cartRepository.Add(cart);
                
            }
            else
            {
                cart = _cartRepository.GetById(user.Cart.Id);

            }

            return  cart.ToCartDto();
        }

        public async Task RemoveProductFromCart(int userId, int productId)
        {

            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            // Get the user's cart
            var cart = _cartRepository.GetById(user.Cart.Id);
            if (cart == null)
            {
                throw new Exception("Cart not found for the user.");
            }

            // Remove product from cart
           await  _cartRepository.RemoveProductFromCartAsync(cart.Id, productId);
        }
    }
}

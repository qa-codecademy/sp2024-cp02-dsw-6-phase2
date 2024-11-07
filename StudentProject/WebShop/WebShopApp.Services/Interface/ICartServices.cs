using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Cart;

namespace WebShopApp.Services.Interface
{
    public interface ICartServices
    {


        List<CartDto> GetAllCarts();

       Task<CartDto> GetUserCart(int userId);
        Task AddProductToCart(int cartId, int productId, int quantity);
        Task RemoveProductFromCart(int userId, int productId);
        Task ClearCart(int userId);
        void DeleteCart (int userId);
        Task<Cart> GetById (int cartId);
       

    }
}

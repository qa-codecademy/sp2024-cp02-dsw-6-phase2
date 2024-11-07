using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.Cart
{
    public class CartDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<CartItem> CartItems;
        public decimal TotalPrice { get; set; }

    }
}

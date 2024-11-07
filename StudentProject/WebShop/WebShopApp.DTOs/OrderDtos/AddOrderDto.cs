using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.OrderDtos
{
    public class AddOrderDto
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Foreign Key for User

        public List<OrderItem> OrderItems { get; set; }



    }
}

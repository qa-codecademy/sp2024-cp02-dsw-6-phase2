using WebShopApp.Domain.Models;
using WebShopApp.DTOs.OrderDtos;




namespace WebShopApp.Services.Interface
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders(bool isOrderForUser); 
        Orderr GetOrderById(int id);
        Task CreateOrder(int userId, AddOrderDto addOrderDto);
        Task UpdateOrder (Orderr updateOrder); 
        Task DeleteOrderById(int id);

        Task AddProductToOrder(int orderId , int productId, int quantity);
        Task RemoveProductFromOrder(int orderId, int productId);
        Task<OrderDto> CreateOrderFromCart(int cartId);





    }
}

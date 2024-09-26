using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Order;

namespace WebShopApp.Services.Interface
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders(); 
        OrderDto GetOrderById(int id);
        Task AddOrder(AddOrderDto addOrderDto);
        Task UpdateOrder (UpdateOrderDto updateOrderDto); 
        Task DeleteOrderById(int id);

        Task AddProductToOrder(int orderId , int productId, int quantity);
        Task RemoveProductFromOrder(int orderId, int productId);
        Task CreateOrderFromCart(Cart cart);





    }
}

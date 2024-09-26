using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Order;
using WebShopApp.Services.Interface;

namespace WebShopApp.Services.Implementation
{
    public class OrderServices : IOrderService
    {
        public Task AddOrder(AddOrderDto addOrderDto)
        {
            throw new NotImplementedException();
        }

        public Task AddProductToOrder(int orderId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderFromCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDto GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductFromOrder(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            throw new NotImplementedException();
        }
    }
}

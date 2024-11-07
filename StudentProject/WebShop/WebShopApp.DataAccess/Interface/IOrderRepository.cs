using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Order;

namespace WebShopApp.DataAccess.Interface
{
    public interface IOrderRepository :IRepository<Orderr>
    {
        Task AddProductToOrderAsync(int orderId, int productId, int quantity);
        Task<Orderr> CreateOrderFromCartAsync(Cart cart);
    }
}

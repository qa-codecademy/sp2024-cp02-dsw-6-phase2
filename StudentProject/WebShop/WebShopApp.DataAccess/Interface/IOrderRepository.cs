using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Interface
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task AddProductToOrderAsync(int orderId, int productId, int quantity);
        Task RemoveProductFromOrderAsync(int orderId, int productId);
        Task CreateOrderFromCartAsync(Cart cart);
    }
}

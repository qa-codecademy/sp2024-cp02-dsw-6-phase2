using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Interface
{
    public interface ICartRepository:IRepository<Cart>
    {
        Task AddProductToCartAsync(int cartId, int productId, int quantity);
        Task RemoveProductFromCartAsync(int cartId, int productId);
        Task ClearCartAsync(int cartId);

    }
}

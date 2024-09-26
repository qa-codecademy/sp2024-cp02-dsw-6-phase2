using Microsoft.EntityFrameworkCore;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly WebAppDbContext _dbContext;

        public OrderRepository(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task AddProductToOrderAsync(int orderId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderFromCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _dbContext.Orders
                    .Include(x => x.Products)
                    .Include(x => x.User)
                    .ToList();
        }

        public Order GetById(int id)
        {
            return _dbContext.Orders
                     .Include(x => x.Products)
                     .Include(x => x.User)
                     .FirstOrDefault(x => x.Id == id);
        }

        public Task RemoveProductFromOrderAsync(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}

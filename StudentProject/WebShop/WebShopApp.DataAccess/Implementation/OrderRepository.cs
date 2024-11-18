using Microsoft.EntityFrameworkCore;
using WebShopApp.DataAccess.Interface;
using WebShopApp.DataAccess.Migrations;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.OrderDtos;

namespace WebShopApp.DataAccess.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly WebAppDbContext _dbContext;

        public OrderRepository(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Orderr entity)
        {
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToOrderAsync(int orderId, int productId, int quantity)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) throw new Exception("Order not found.");


            var orderItems = order.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);


            if (orderItems == null)
            {
                var product = await _dbContext.Products.FindAsync(productId);
                if (product == null) throw new Exception("Product not found.");

                orderItems = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                };

                order.OrderItems.Add(orderItems);
            }
            else
            {
                orderItems.Quantity += quantity;
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity);

            await _dbContext.SaveChangesAsync();




        }



        public void Delete(Orderr entity)
        {
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Orderr> GetAll()
        {
            return _dbContext.Orders
                 .Include(o => o.OrderItems)
                 .ToList();
        }

        public Orderr GetById(int id)
        {
            return _dbContext.Orders
                     .Include(x => x.OrderItems)
                     .ThenInclude(oi => oi.Product)
                     .Include(x => x.User)
                     .FirstOrDefault(x => x.Id == id);
        }

       

        public async Task Update(Orderr entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.OrderItems == null || !entity.OrderItems.Any())
                throw new InvalidOperationException("OrderItems cannot be null or empty.");

            // Calculate TotalAmount
            entity.TotalAmount = entity.OrderItems
                .Where(oi => oi.Product != null) // Filter out null products
                .Sum(oi => oi.Product.Price * oi.Quantity);

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }



          public async Task<Orderr> CreateOrderFromCartAsync(Cart cart)
        {
            var cartDb = await _dbContext.Carts
                .Include(x => x.User)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == cart.Id);


            if (cartDb == null || !cartDb.CartItems.Any())
            {
                throw new InvalidOperationException("The cart is empty or doesn't exist.");
            }

            // Create a new Order
            var order = new Orderr
            {
                OrderDate = DateTime.Now,
                Address= cartDb.User.Address,
                User = cartDb.User,
                UserId = cartDb.UserId,
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0 // Initialize total amount
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Map CartItems to OrderItems
            foreach (var cartItem in cartDb.CartItems)
            {
                var existingOrderItem = await _dbContext.OrderItems
         .FirstOrDefaultAsync(oi => oi.OrderId == order.Id && oi.ProductId == cartItem.ProductId);


                if (existingOrderItem != null)
                {
                    // Update the quantity if the product is already in the order
                    existingOrderItem.Quantity += cartItem.Quantity;
                }
                else { 
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity,
                    OrderId = order.Id

                };
                    order.OrderItems.Add(orderItem);

                }

               
                order.TotalAmount = order.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity);
            }
               
            await _dbContext.SaveChangesAsync();
            
            return order;

        }

        public List<Orderr> GetOrdersByUserId(int userId)
        {
            return _dbContext.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}

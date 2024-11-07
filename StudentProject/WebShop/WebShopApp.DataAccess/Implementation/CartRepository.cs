using Microsoft.EntityFrameworkCore;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly WebAppDbContext _dbContext;
        
        public CartRepository(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Cart entity)
        {
            _dbContext.Carts.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _dbContext.Carts
                .Include(x => x.User)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null) throw new Exception("Cart not found.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                var product = await _dbContext.Products.FindAsync(productId);
                if (product == null) throw new Exception("Product not found.");

                cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                };

                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            cart.TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(c => c.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart != null)
            {
                cart.CartItems.Clear();
                cart.TotalAmount = 0;
                await _dbContext.SaveChangesAsync();
            }
        }

        public void Delete(Cart entity)
        {
            _dbContext.Carts.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Cart> GetAll()
        {
            return _dbContext.Carts
                .Include(c => c.CartItems)
                .ToList();
        }

        public Cart GetById(int id)
        {
            return _dbContext.Carts.Include(x => x.User).Include(c => c.CartItems).ThenInclude(c => c.Product).FirstOrDefault(x => x.Id == id);
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
           return  _dbContext.Carts.Include(x => x.User).Include(c => c.CartItems).ThenInclude(c => c.Product).FirstOrDefault(x => x.Id == cartId);

        }

        public async Task RemoveProductFromCartAsync(int cartId, int productId)
        {
            var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(c => c.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null) throw new Exception("Cart not found.");

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                throw new Exception("The product is not in the cart");
            }


            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cart.CartItems.Remove(cartItem);
            }

            cart.TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);
                await _dbContext.SaveChangesAsync();
            
        }

        public async Task Update(Cart entity)
        {
            _dbContext.Update(entity);
          await  _dbContext.SaveChangesAsync();
        }
    }
}

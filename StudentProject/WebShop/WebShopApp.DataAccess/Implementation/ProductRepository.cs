using Microsoft.EntityFrameworkCore;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebAppDbContext _dbContext;
        public ProductRepository(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Product entity)
        {
            _dbContext.Products.Add(entity);
           await _dbContext.SaveChangesAsync();
        }

        public void Delete(Product entity)
        {
            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Product> FilterProducts(int? category, string? brand)
        {
            if (brand == null)
            {
                return _dbContext.Products.Where(x => (int)x.Category == category.Value).ToList();
            }
            if (category == null)
            {
                return _dbContext.Products.Where(x => x.Brand.ToLower() == brand.ToLower()).ToList();   
            }

            return _dbContext.Products.Where(x => x.Brand.ToLower() == brand.ToLower() && (int)x.Category == category.Value).ToList();
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products.Include(x => x.Orders).ToList();
        }

        public Product GetById(int id)
        {
         return   _dbContext.Products.Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(Product entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

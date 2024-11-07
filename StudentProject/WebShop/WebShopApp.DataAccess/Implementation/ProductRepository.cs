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

        public async Task Add(Productt entity)
        {
            _dbContext.Products.Add(entity);
           await _dbContext.SaveChangesAsync();
        }

        public void Delete(Productt entity)
        {
            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Productt> FilterProducts(int? category, string? brand)
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

        public List<Productt> GetAll()
        {
            return _dbContext.Products.ToList();
            
        }

        public Productt GetById(int id)
        {
            return   _dbContext.Products.FirstOrDefault(x => x.Id == id);

        }

        public async Task Update(Productt entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

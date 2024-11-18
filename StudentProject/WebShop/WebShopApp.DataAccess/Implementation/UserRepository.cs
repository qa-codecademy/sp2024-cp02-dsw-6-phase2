using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly WebAppDbContext _dbContext;

        public UserRepository(WebAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       


        public async Task Add(Userr entity)
        {
            _dbContext.Users.Add(entity);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"An error occurred: {ex.InnerException?.Message}");
            }
        }

        public void Delete(Userr entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Userr> GetAll()
        {
            return _dbContext.Users
                .Include(x => x.Orders)
                .Include(x => x.Cart)
                .ToList();
        }

        public Userr GetById(int id)
        {
            return _dbContext.Users.Include(x => x.Cart).Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        }

        public Userr GetUserByUserName(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == userName);

        }

        public Userr GetUserByUserNameAndPassword(string userName, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);

        }

        public async Task Update(Userr entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            
        }
    }
}

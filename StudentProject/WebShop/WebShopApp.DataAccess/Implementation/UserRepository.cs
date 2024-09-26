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

       


        public async Task Add(User entity)
        {
            _dbContext.Users.Add(entity);
           await  _dbContext.SaveChangesAsync();   
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _dbContext.Users
                .Include(x => x.Orders)
                .Include(x => x.Cart)
                .ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.Include(x => x.Cart).Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUserName(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == userName);

        }

        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);

        }

        public async Task Update(User entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            
        }
    }
}

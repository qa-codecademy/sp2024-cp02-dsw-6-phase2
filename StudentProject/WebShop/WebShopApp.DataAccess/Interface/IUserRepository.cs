using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DataAccess.Interface
{
    public interface IUserRepository :IRepository<User>
    {
        User GetUserByUserName(string userName);
        User GetUserByUserNameAndPassword(string userName, string password);


    }
}

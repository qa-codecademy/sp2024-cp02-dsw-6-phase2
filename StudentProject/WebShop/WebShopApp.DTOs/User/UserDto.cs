using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Enums.UserEnum;
using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.User
{
    public class UserDto
    {
     
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public string Phone { get; set; }
       
        public List<Order> Orders { get; set; }
        
    }
}

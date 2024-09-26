using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Enums.UserEnum;

namespace WebShopApp.DTOs.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}

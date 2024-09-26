using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.User;

namespace WebShopApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUserDto , string hash)
        {
            return new User
            {
                Name = registerUserDto.Name,
                LastName = registerUserDto.LastName,
                Address = registerUserDto.Address,
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                Role = registerUserDto.Role,
                Phone = registerUserDto.Phone,
                Password  = hash
            };
        }



        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
              Name= user.Name,
              LastName= user.LastName,
              Address = user.Address,
              UserName = user.UserName,
              Email = user.Email,
              Role = user.Role,
              Phone = user.Phone,
              Orders = user.Orders
            };
        }
    }
}

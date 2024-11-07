using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.DTOs.Product;
using WebShopApp.DTOs.User;

namespace WebShopApp.Services.Interface
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        UserDto GetUserById(int id);

        Task RegisterUser(RegisterUserDto registerUserDto);
        LoginResponse Login(LoginUserDto loginUserDto);

        void UpdateUser(UpdateUserDto updateUserDto);
        void DeleteUsert(int id);
        List<UserDto> FilterUsers();
    }
}

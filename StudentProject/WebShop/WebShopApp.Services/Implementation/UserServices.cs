using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.User;
using WebShopApp.Mappers;
using WebShopApp.Services.Interface;
using WebShopApp.Shared;
using XSystem.Security.Cryptography;

namespace WebShopApp.Services.Implementation
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void DeleteUsert(int id)
        {
            Userr userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception("User not found");

            }
            _userRepository.Delete(userDb);
        }

        public List<UserDto> FilterUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(int id)
        {
            Userr userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new Exception("User not found");

            }

            UserDto userDto = userDb.ToUserDto();
            return userDto;

        }

        public List<UserDto> GetUsers()
        {
            var usersDb = _userRepository.GetAll();
            return usersDb.Select(x => x.ToUserDto()).ToList();
        }

        public LoginResponse Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                throw new DataException("User cannt be null");

            }
            if (string.IsNullOrEmpty(loginUserDto.UserName) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new DataException("Username and password are reqired");

            }

            string hash = GennerateHash(loginUserDto.Password);
            //we send the hash passwod cuz that is saved in the db and what we need to comapre

            Userr userDb = _userRepository.GetUserByUserNameAndPassword(loginUserDto.UserName, hash);

            if (userDb == null)
            {
                throw new Exception("Not found");

            }


            //generate JWT that will be returned to the client

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our secret secretttt secret  key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(5),//valid for 5 hours

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes)
                                                                , SecurityAlgorithms.HmacSha256Signature),
                //payload part
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    //things that will be stored in the token about the user
                    {
                        new Claim("userFullName" , userDb.Name + ' ' + userDb.LastName),
                        new Claim(ClaimTypes.NameIdentifier, userDb.UserName),
                        new Claim("userId", userDb.Id.ToString()),
                        new Claim(ClaimTypes.Role , userDb.Role.ToString())
                    }
                    )


            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);


            ///converet to string 

            string resultToken = jwtSecurityTokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Token = resultToken,
                ValidTo =token.ValidTo
            };


        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                throw new Exception("User cant be null");
            }

            ValidationHelper.ValidateReqiredStrings(registerUserDto.Name, "Name", 50);
            ValidationHelper.ValidateReqiredStrings(registerUserDto.LastName, "LastName", 70);
            ValidationHelper.ValidateReqiredStrings(registerUserDto.Address, "Address", 150);
            ValidationHelper.ValidateReqiredStrings(registerUserDto.UserName, "UserName", 50);
            ValidationHelper.ValidateReqiredStrings(registerUserDto.Email, "Email", 200);
            ValidationHelper.ValidateReqiredStrings(registerUserDto.Phone, "Phone", 20);

            if (string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new DataException("Password is reqired");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new DataException("Password must match");

            }


            Userr userDb = _userRepository.GetUserByUserName(registerUserDto.UserName);
            if (userDb != null)
            {
                throw new DataException($"Username {registerUserDto.UserName} is already in use");
            }



            string hash = GennerateHash(registerUserDto.Password);
            Userr newUser = registerUserDto.ToUser(hash);
            await _userRepository.Add(newUser);



        }

        public void UpdateUser(UpdateUserDto updateUserDto)
        {
            Userr userDb = _userRepository.GetById(updateUserDto.Id);
            if(userDb == null)
            {
                throw new Exception("User not found");

            }

            ValidationHelper.ValidateReqiredStrings(updateUserDto.Name, "Name", 50);
            ValidationHelper.ValidateReqiredStrings(updateUserDto.LastName, "LastName", 70);
            ValidationHelper.ValidateReqiredStrings(updateUserDto.Address, "Address", 150);
            ValidationHelper.ValidateReqiredStrings(updateUserDto.Email, "Email", 200);
            ValidationHelper.ValidateReqiredStrings(updateUserDto.Phone, "Phone", 20);

            if (string.IsNullOrEmpty(updateUserDto.Password) || string.IsNullOrEmpty(updateUserDto.ConfirmPassword))
            {
                throw new DataException("Password is reqired");
            }

            if (updateUserDto.Password != updateUserDto.ConfirmPassword)
            {
                throw new DataException("Password must match");

            }

            string hash = GennerateHash(updateUserDto.Password);

             _userRepository.Update(userDb);
            userDb.Name = updateUserDto.Name;
            userDb.LastName = updateUserDto.LastName;
            userDb.Address = updateUserDto.Address;
            userDb.Email = updateUserDto.Email;
            userDb.Role = updateUserDto.Role;
            userDb.Phone = updateUserDto.Phone;
            userDb.Password = hash;



            _userRepository.Update(userDb);



        }














        private static string GennerateHash(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();


            //from string to byte : ex . Test123 -> 1155998774
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);


            //from bytes to hash byte : ex . 1155998774 -> 56565656
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);


            //get the string from hash bytes : 655456465 -> ds515d5
            string hash = Encoding.ASCII.GetString(hashedBytes);
            return hash;

        }
    }
}

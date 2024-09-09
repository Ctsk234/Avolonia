using BooksManager.DTO;
using BooksManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Service
{
    public class AuthService
    {
        private readonly UserService _userService;

        public AuthService(UserService userService) 
        {
            _userService = userService;
        }

        public User Login(LoginRequest loginRequest)
        {
            var user = _userService.GetUser(loginRequest.Username);

            if(user == null)
                return null;

            if(BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
                return user;

            return null;
        }

        public Task<User> Register(LoginRequest registerRequest)
        {
            var user = _userService.GetUser(registerRequest.Username);

            if (user != null)
                return Task.FromResult<User>(null);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            return _userService.CreateUser(registerRequest.Username, passwordHash);
        }
    }
}

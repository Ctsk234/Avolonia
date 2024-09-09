using BooksManager.Model;
using BooksManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Service
{
    public class UserService
    {
        private readonly IAppDbContext _appDbContext;

        public UserService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _appDbContext.Users;
        }

        public User? GetUser(string name)
        {
            try
            {
                // prevents from crash if users table is empty
                return _appDbContext.Users.First(u => u.Name == name); 
            } catch (Exception e) { }

            return null;
        } 

        public async Task<User> CreateUser(string name, string passwordHash) 
        {
            var newUser = new User()
            {
                Name = name,
                PasswordHash = passwordHash
            };
            var result = _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> RemoveUser(int id)
        {
            var userToRemove = _appDbContext.Users.First(u => u.Id == id);
            var res = _appDbContext.Users.Remove(userToRemove);
            await _appDbContext.SaveChangesAsync();
            return res != null;
        }

    }
}

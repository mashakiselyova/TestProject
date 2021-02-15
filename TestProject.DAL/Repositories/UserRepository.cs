using TestProject.DAL.Contexts;
using TestProject.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TestProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public User GetUser(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void Update(User user)
        {
             _context.Users.Update(user);
        }
    }
}

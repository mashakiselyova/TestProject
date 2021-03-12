using TestProject.DAL.Contexts;
using TestProject.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.SingleAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

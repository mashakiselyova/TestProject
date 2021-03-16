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

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleAsync(u => u.Email == email);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}

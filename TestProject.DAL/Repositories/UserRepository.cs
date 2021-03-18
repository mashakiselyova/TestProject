using TestProject.DAL.Contexts;
using TestProject.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TestProject.DAL.Repositories
{
    public class UserRepository : IRepository<User>
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

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await FindById(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public List<User> Get(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }
    }
}

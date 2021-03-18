using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private ApplicationContext _context;

        public PostRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> Get()
        {
            return await _context.Posts.Include(p => p.User).ToListAsync();
        }

        public async Task<Post> FindById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var post = await FindById(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public List<Post> Get(Func<Post, bool> predicate)
        {
            return _context.Posts.Include(p => p.User).Where(predicate).ToList();
        }
    }
}

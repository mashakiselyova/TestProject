using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ApplicationContext _context;

        public PostRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.User)
                .OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public async Task<Post> GetAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var post = await GetAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }       
    }
}

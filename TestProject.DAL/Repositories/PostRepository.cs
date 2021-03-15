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

        public async Task Create(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.Include(p => p.User)
                .OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public async Task<List<Post>> GetUserPosts(int id)
        {
            return await _context.Posts.Where(p => p.UserId == id).Include(p => p.User)
                .OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public async Task<Post> Get(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var post = await Get(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        
    }
}

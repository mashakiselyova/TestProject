using System;
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

        public Task UpdateAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}

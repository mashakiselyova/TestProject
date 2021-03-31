using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private ApplicationContext _context;

        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await FindById(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> FindById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> Get()
        {
            return await _context.Comments.Include(c => c.User)
            .Include(c => c.CommentRatings).AsNoTracking().ToListAsync();
        }

        public List<Comment> Get(Func<Comment, bool> predicate)
        {
            return _context.Comments.Include(c => c.User)
            .Include(c => c.CommentRatings).AsNoTracking()
            .Where(predicate).ToList();
        }

        public async Task Update(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}

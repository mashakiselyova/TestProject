using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class CommentRatingRepository : IRepository<CommentRating>
    {
        private ApplicationContext _context;

        public CommentRatingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(CommentRating rating)
        {
            await _context.CommentRatings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rating = await FindById(id);
            _context.CommentRatings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentRating> FindById(int id)
        {
            return await _context.CommentRatings.FindAsync(id);
        }

        public async Task<List<CommentRating>> Get()
        {
            return await _context.CommentRatings.ToListAsync();
        }

        public List<CommentRating> Get(Func<CommentRating, bool> predicate)
        {
            return _context.CommentRatings.Where(predicate).ToList();
        }

        public async Task Update(CommentRating rating)
        {
            _context.CommentRatings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}

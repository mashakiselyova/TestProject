using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class PostRatingRepository : IRepository<PostRating>
    {
        private ApplicationContext _context;

        public PostRatingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(PostRating rating)
        {
            await _context.PostRatings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rating = await FindById(id);
            _context.PostRatings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<PostRating> FindById(int id)
        {
            return await _context.PostRatings.FindAsync(id);
        }

        public async Task<List<PostRating>> Get()
        {
            return await _context.PostRatings.ToListAsync();
        }

        public List<PostRating> Get(Func<PostRating, bool> predicate)
        {
            return _context.PostRatings.Include(r => r.Post).Where(predicate).ToList();
        }

        public async Task Update(PostRating rating)
        {
            _context.PostRatings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.DAL.Contexts;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private ApplicationContext _context;

        public RatingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rating = await FindById(id);
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<Rating> FindById(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<List<Rating>> Get()
        {
            return await _context.Ratings.ToListAsync();
        }

        public List<Rating> Get(Func<Rating, bool> predicate)
        {
            return _context.Ratings.Include(r => r.Post).Where(predicate).ToList();
        }

        public async Task Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }
    }
}

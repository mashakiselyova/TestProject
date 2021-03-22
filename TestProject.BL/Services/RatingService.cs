using System;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Utils;
using TestProject.DAL.Enums;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;

namespace TestProject.BL.Services
{
    public class RatingService : IRatingService
    {
        private IRepository<Rating> _ratingRepository;
        private IRepository<User> _userRepository;
        private IMapper<RatingModel, Rating> _ratingMapper;

        public RatingService(IRepository<Rating> ratingRepository, 
            IRepository<User> userRepository, 
            IMapper<RatingModel, Rating> ratingMapper)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _ratingMapper = ratingMapper;
        }        

        public async Task Set(RatingModel ratingModel, string email)
        {
            var user = _userRepository.GetByEmail(email);
            var rating = _ratingMapper.ToDalModel(ratingModel);
            rating.UserId = user.Id;
            await _ratingRepository.Create(rating);
        }

        public bool CheckIfRated(int postId, string email)
        {
            var user = _userRepository.GetByEmail(email);
            var ratings = _ratingRepository.Get(r => r.PostId == postId && r.UserId == user.Id);
            return ratings.Count > 0;
        }

        public int Get(int postId)
        {
            return CalculatePostRating(postId);            
        }

        private int CalculatePostRating(int postId)
        {
            var ratings = _ratingRepository.Get(r => r.PostId == postId);
            var pluses = ratings.Where(r => r.Value == RatingValue.Plus).ToList().Count();
            var minuses = ratings.Where(r => r.Value == RatingValue.Minus).ToList().Count();
            return pluses - minuses;
        }
    }
}

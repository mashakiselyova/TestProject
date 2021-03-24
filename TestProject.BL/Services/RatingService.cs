using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Exceptions;
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

        /// <summary>
        /// Sets post's rating
        /// </summary>
        /// <param name="ratingModel">Rating</param>
        /// <param name="email">User email</param>
        public async Task Set(RatingModel ratingModel, string email)
        {
            var userId = _userRepository.GetByEmail(email).Id;
            if (ratingModel.AuthorId == userId)
            {
                throw new RatingFailedException();
            }
            var ratingByCurrentUser = GetRatingByUser(userId, ratingModel.PostId);
            if (ratingByCurrentUser == ratingModel.Value)
            {
                await Delete(userId, ratingModel.PostId);
            }
            else
            {
                await CreateOrUpdate(ratingModel, userId, ratingByCurrentUser);
            }
        }

        /// <summary>
        /// Updates rating if it was set or creates a new one
        /// </summary>
        /// <param name="ratingModel">Rating</param>
        /// <param name="userId">Current user Id</param>
        /// <param name="ratingByCurrentUser">Rating by current user</param>
        private async Task CreateOrUpdate(RatingModel ratingModel, int userId, RatingValue ratingByCurrentUser)
        {
            if (ratingByCurrentUser == RatingValue.Unrated)
            {
                await Create(ratingModel, userId);
            }
            else
            {
                await Update(ratingModel, userId);
            }
        }

        /// <summary>
        /// Updates rating
        /// </summary>
        /// <param name="ratingModel">Rating</param>
        /// <param name="userId">Current user id</param>
        private async Task Update(RatingModel ratingModel, int userId)
        {
            var rating = _ratingRepository.Get(r => r.PostId == ratingModel.PostId && r.UserId == userId).Single();
            rating.Value = ratingModel.Value;
            await _ratingRepository.Update(rating);
        }

        /// <summary>
        /// Create new rating
        /// </summary>
        /// <param name="ratingModel">Rating</param>
        /// <param name="userId">Current user Id</param>
        private async Task Create(RatingModel ratingModel, int userId)
        {
            var rating = _ratingMapper.ToDalModel(ratingModel);
            rating.UserId = userId;
            await _ratingRepository.Create(rating);
        }

        /// <summary>
        /// Gets rating by current user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="postId">Post Id</param>
        /// <returns>Rating Value</returns>
        private RatingValue GetRatingByUser(int userId, int postId)
        {
            var ratings = _ratingRepository.Get(r => r.PostId == postId && r.UserId == userId);
            if (ratings.Count == 0)
            {
                return RatingValue.Unrated;
            }
            else
            {
                return ratings.Single().Value;
            }
        }

        /// <summary>
        /// Deletes rating
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="postId">Post Id</param>
        /// <returns></returns>
        private async Task Delete(int userId, int postId)
        {
            var rating = _ratingRepository.Get(r => r.PostId == postId && r.UserId == userId).Single();
            await _ratingRepository.Delete(rating.Id);
        }
    }
}

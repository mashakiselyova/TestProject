using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Enums;
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
        private IRepository<Post> _postRepository;
        private IMapper<RatingModel, Rating> _ratingMapper;

        public RatingService(IRepository<Rating> ratingRepository, 
            IRepository<User> userRepository,
            IRepository<Post> postRepository,
            IMapper<RatingModel, Rating> ratingMapper)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
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
            var authorId = (await _postRepository.FindById(ratingModel.PostId)).UserId;
            if (authorId == userId)
            {
                throw new RatingFailedException("Author cannot set rating");
            }
            var ratingByCurrentUser = GetRatingByUser(userId, ratingModel.PostId);
            await ProcessRatingSet(ratingModel, userId, ratingByCurrentUser);
        }

        /// <summary>
        /// Creates rating if it doesn't exist
        /// Updates rating if it has changed
        /// Deletes rating if it was cancelled
        /// </summary>
        /// <param name="ratingModel">Rating model</param>
        /// <param name="userId">Current user Id</param>
        /// <param name="ratingByCurrentUser">Rating by current user</param>
        private async Task ProcessRatingSet(RatingModel ratingModel, int userId, Rating ratingByCurrentUser)
        {
            if (ratingByCurrentUser == null)
            {
                await Create(ratingModel, userId);
            }
            else if (ratingByCurrentUser.Value == (RatingValue)ratingModel.Value)
            {
                await _ratingRepository.Delete(ratingByCurrentUser.Id);
            }
            else
            {
                ratingByCurrentUser.Value = (RatingValue)ratingModel.Value;
                await _ratingRepository.Update(ratingByCurrentUser);
            }
        }

        /// <summary>
        /// Gets total rating and rating by current user
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <param name="email">Current user email</param>
        /// <returns>UpdateRatingModel</returns>
        public UpdateRatingModel GetUpdatedRating(int postId, string email)
        {
            var userId = _userRepository.GetByEmail(email).Id;
            var ratingByCurrentUser = GetRatingByUser(userId, postId);
            var totalRating = GetTotalRating(postId);
            return new UpdateRatingModel
            {
                TotalRating = totalRating,
                RatingByCurrentUser = ratingByCurrentUser == null 
                    ? RatingOption.Unrated : (RatingOption)ratingByCurrentUser.Value
            };
        }

        /// <summary>
        /// Gets total rating for post
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <returns>Total Rating</returns>
        private int GetTotalRating(int postId)
        {
            var ratings = _ratingRepository.Get(r => r.PostId == postId);
            return RatingHelper.CalculateRating(ratings);
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
        private Rating GetRatingByUser(int userId, int postId)
        {
            var ratings = _ratingRepository.Get(r => r.PostId == postId && r.UserId == userId);
            return ratings.SingleOrDefault();
        }
    }
}

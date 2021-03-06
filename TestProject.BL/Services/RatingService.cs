﻿using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Exceptions;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Utils;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using TestProject.Enums;

namespace TestProject.BL.Services
{
    public class RatingService : IRatingService
    {
        private IRepository<PostRating> _ratingRepository;
        private IRepository<User> _userRepository;
        private IRepository<Post> _postRepository;
        private IMapper<RatingModel, PostRating> _ratingMapper;

        public RatingService(IRepository<PostRating> ratingRepository, 
            IRepository<User> userRepository,
            IRepository<Post> postRepository,
            IMapper<RatingModel, PostRating> ratingMapper)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _ratingMapper = ratingMapper;
        }

        /// <summary>
        /// Creates rating if it doesn't exist
        /// Updates rating if it has changed
        /// Deletes rating if it was cancelled
        /// </summary>
        /// <param name="ratingModel">Rating</param>
        /// <param name="currentUserEmail">Current user email</param>
        public async Task Set(RatingModel ratingModel, string currentUserEmail)
        {
            var userId = _userRepository.GetByEmail(currentUserEmail).Id;
            var authorId = (await _postRepository.FindById(ratingModel.PostId)).UserId;
            if (authorId == userId)
            {
                throw new RatingFailedException("Author cannot set rating");
            }
            var ratingByCurrentUser = GetRatingByUser(userId, ratingModel.PostId);
            if (ratingByCurrentUser == null)
            {
                await Create(ratingModel, userId);
            }
            else
            {
                await Update(ratingModel, ratingByCurrentUser);
            }
        }

        /// <summary>
        /// Gets total rating and rating by current user
        /// </summary>
        /// <param name="postId">Post Id</param>
        /// <param name="currentUserEmail">Current user email</param>
        /// <returns>UpdateRatingModel</returns>
        public UpdateRatingModel GetUpdatedRating(int postId, string currentUserEmail)
        {
            var userId = _userRepository.GetByEmail(currentUserEmail).Id;
            var ratingByCurrentUser = GetRatingByUser(userId, postId);
            var totalRating = GetTotalRating(postId);
            return new UpdateRatingModel
            {
                TotalRating = totalRating,
                RatingByCurrentUser = ratingByCurrentUser == null 
                    ? RatingValue.Unrated : ratingByCurrentUser.Value
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
        private PostRating GetRatingByUser(int userId, int postId)
        {
            var ratings = _ratingRepository.Get(r => r.PostId == postId && r.UserId == userId);
            return ratings.SingleOrDefault();
        }

        private async Task Update(RatingModel ratingModel, PostRating ratingByCurrentUser)
        {
            if (ratingModel.Value == RatingButtonPosition.ThumbsUp)
            {
                await ProcessRatingButton(ratingByCurrentUser, RatingValue.Plus);
            }
            else
            {
                await ProcessRatingButton(ratingByCurrentUser, RatingValue.Minus);
            }
        }

        private async Task ProcessRatingButton(PostRating ratingByCurrentUser, RatingValue ratingValue)
        {
            if (ratingByCurrentUser.Value == ratingValue)
            {
                await _ratingRepository.Delete(ratingByCurrentUser.Id);
            }
            else
            {
                ratingByCurrentUser.Value = ratingValue;
                await _ratingRepository.Update(ratingByCurrentUser);
            }
        }
    }
}

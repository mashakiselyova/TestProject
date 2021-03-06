﻿using TestProject.DAL.Repositories;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Mappers;
using TestProject.DAL.Models;
using System.Linq;
using TestProject.BL.Utils;
using System;
using TestProject.BL.Exceptions;

namespace TestProject.BL.Services
{
    /// <summary>
    /// Class for working with users
    /// </summary>
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IRepository<PostRating> _ratingRepository;
        private IMapper<UserLoginModel, User> _userLoginMapper;
        private IMapper<UserProfile, User> _userProfileMapper;

        public UserService(IRepository<User> repository,
            IRepository<PostRating> ratingRepository,
            IMapper<UserLoginModel, User> userLoginMapper,
            IMapper<UserProfile, User> userProfileMapper)
        {
            _userRepository = repository;
            _ratingRepository = ratingRepository;
            _userLoginMapper = userLoginMapper;
            _userProfileMapper = userProfileMapper;
        }

        /// <summary>
        /// Adds a new user or updates an existing one
        /// </summary>
        /// <param name="userLoginModel">User</param>
        public async Task AddOrUpdate(UserLoginModel userLoginModel)
        {
            if (DoesExist(userLoginModel.Email))
            {
                await Update(userLoginModel);
            }
            else
            {
                await Create(userLoginModel);
            }
        }

        /// <summary>
        /// Gets current user profile
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User profile</returns>
        public UserProfile GetByEmail(string email)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);
                return RetrieveProfile(user);
            }
            catch (InvalidOperationException)
            {
                throw new UserNotFoundException();
            }            
        }        

        /// <summary>
        /// Gets user profile
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User Profile</returns>
        public UserProfile Get(int id)
        {
            var user = _userRepository.Get(u => u.Id == id).SingleOrDefault();
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return RetrieveProfile(user);
        }

        private UserProfile RetrieveProfile(User user)
        {
            var userProfile = _userProfileMapper.ToBlModel(user);
            userProfile.Rating = CalculateRating(user.Id);
            return userProfile;
        }

        /// <summary>
        /// Checks if user data has changed
        /// </summary>
        /// <param name="userLoginModel">User login data</param>
        /// <param name="user">User data from database</param>
        private bool HasChanged(UserLoginModel userLoginModel, User user)
        {
            return !(userLoginModel.FirstName == user.FirstName && userLoginModel.LastName == user.LastName);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userLoginModel">User login data</param>
        private async Task Create(UserLoginModel userLoginModel)
        {
            var user = _userLoginMapper.ToDalModel(userLoginModel);
            await _userRepository.Create(user);
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="userLoginModel">User login data</param>
        private async Task Update(UserLoginModel userLoginModel)
        {
            var user = _userRepository.GetByEmail(userLoginModel.Email);
            if (!HasChanged(userLoginModel, user))
            {
                return;
            }
            var newUser = _userLoginMapper.ToDalModel(userLoginModel);
            newUser.Id = user.Id;
            await _userRepository.Update(newUser);
        }

        /// <summary>
        /// Checks if user is in the database
        /// </summary>
        /// <param name="email">User email</param>
        private bool DoesExist(string email)
        {
            return _userRepository.Get(u => u.Email == email).SingleOrDefault() != null;
        }

        /// <summary>
        /// Calculates user's rating
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Rating</returns>
        private int CalculateRating(int userId)
        {
            var ratings = _ratingRepository.Get(r => r.Post.UserId == userId);
            return RatingHelper.CalculateRating(ratings);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Class for working with posts
    /// </summary>
    public class PostService : IPostService
    {
        private IRepository<Post> _postRepository;
        private IRepository<User> _userRepository;
        private IMapper<EditPostModel, Post> _editPostMapper;
        private IMapper<PostModel, Post> _postMapper;
        private IMapper<RichPostModel, Post> _richPostMapper;

        public PostService(IRepository<Post> postRepository, IRepository<User> userRepository,
            IMapper<EditPostModel, Post> editPostMapper,
            IMapper<PostModel, Post> postMapper,
            IMapper<RichPostModel, Post> richPostMapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _editPostMapper = editPostMapper;
            _postMapper = postMapper;
            _richPostMapper = richPostMapper;
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="editPostModel">New post</param>
        /// <param name="userEmail">Author's email</param>
        public async Task Create(EditPostModel editPostModel, string userEmail)
        {
            var post = _editPostMapper.ToDalModel(editPostModel);
            var user = _userRepository.GetByEmail(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = post.CreateDate;
            await _postRepository.Create(post);
        }

        /// <summary>
        /// Gets all posts or user's posts
        /// </summary>
        /// <param name="userId">Author's id</param>
        /// <returns>List of posts</returns>
        public async Task<List<PostModel>> GetAll(int? userId, string email)
        {
            var postModelList = (await GetFilteredPosts(userId)).Select(_postMapper.ToBlModel).ToList();
            if (email != null)
            {
                var currentUserId = _userRepository.GetByEmail(email).Id;
                SetSelectedRating(currentUserId, postModelList);
            }            
            CalculateTotalRating(postModelList);
            return postModelList;
        }        

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Post</returns>
        public async Task<EditPostModel> GetById(int id)
        {
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                throw new PostNotFoundException();
            }
            return _editPostMapper.ToBlModel(post);
        }

        /// <summary>
        /// Edits an existing post
        /// </summary>
        /// <param name="editPostModel">Post</param>
        /// <param name="userEmail">Author's email</param>
        public async Task Edit(EditPostModel editPostModel, string userEmail)
        {
            var currentUser = _userRepository.GetByEmail(userEmail);
            var post = await _postRepository.FindById(editPostModel.Id);
            if (post.UserId != currentUser.Id)
            {
                throw new EditFailedException();
            }
            post.Title = editPostModel.Title;
            post.Content = editPostModel.Content;
            post.UpdateDate = DateTime.Now;
            await _postRepository.Update(post);
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="id">Post id</param>
        public async Task Delete(int id)
        {
            await _postRepository.Delete(id);
        }

        /// <summary>
        /// Gets post with user and rating
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <param name="userEmail">Current user email</param>
        /// <returns>RichPostModel</returns>
        public RichPostModel GetRichPost(int id, string userEmail)
        {
            var post = _postRepository.Get(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                throw new PostNotFoundException();
            }
            var richPost = _richPostMapper.ToBlModel(post);
            richPost.TotalRating = RatingHelper.CalculateRating(post.Ratings);
            if (userEmail != null)
            {
                var currentUserId = _userRepository.GetByEmail(userEmail).Id;
                richPost.SelectedRating = GetSelectedRating(currentUserId, post.Ratings);
            }
            return richPost;
        }

        /// <summary>
        /// Gets filtered posts
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Posts</returns>
        private async Task<List<Post>> GetFilteredPosts(int? userId)
        {
            if (userId.HasValue)
            {
                return _postRepository.Get(p => p.UserId == userId.Value);
            }
            else
            {
                return await _postRepository.Get();
            }
        }

        /// <summary>
        /// Calculates and sets total rating
        /// </summary>
        /// <param name="postModelList">Post</param>
        private static void CalculateTotalRating(List<PostModel> postModelList)
        {
            foreach (var post in postModelList)
            {
                post.TotalRating = RatingHelper.CalculateRating(post.Ratings);
            }
        }

        /// <summary>
        /// Sets rating by current user
        /// </summary>
        /// <param name="currentUserId">Current user id</param>
        /// <param name="postModelList">Post</param>
        private void SetSelectedRating(int currentUserId, List<PostModel> postModelList)
        {
            foreach (var post in postModelList)
            {
                post.SelectedRating = GetSelectedRating(currentUserId, post.Ratings);
            }
        }   
        
        private RatingValue GetSelectedRating(int currentUserId, List<PostRating> ratings)
        {
            var selectedRating = ratings.Where(r => r.UserId == currentUserId).SingleOrDefault();
            return selectedRating == null ? RatingValue.Unrated : selectedRating.Value;
        }
    }
}

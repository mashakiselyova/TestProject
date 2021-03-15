﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;

namespace TestProject.BL.Services
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task Create(CreatePostModel postEditorModel, string userEmail)
        {
            var post = PostMapper.MapCreatePostModelToPost(postEditorModel);
            var user = await _userRepository.GetUserAsync(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.Create(post);
        }

        public async Task<List<PostModel>> GetAll()
        {
            var posts = await _postRepository.GetAllPosts();
            var postDisplayModels = posts.Select(PostMapper.MapPostToPostModel).ToList();
            return postDisplayModels;
        }

        public async Task<List<PostModel>> GetPosts(int? id)
        {
            var posts = new List<Post>();
            if (id == null)
            {
                posts = await _postRepository.GetAllPosts();
            }
            else
            {
                posts = await _postRepository.GetUserPosts(id.Value);
            }
            var postDisplayModels = posts.Select(PostMapper.MapPostToPostModel).ToList();
            return postDisplayModels;
        }

        public async Task<EditPostModel> Get(int id)
        {
            var post = await _postRepository.Get(id);
            return PostMapper.MapPostToEditPostModel(post);
        }

        public async Task Edit(EditPostModel editPostModel)
        {
            var post = await _postRepository.Get(editPostModel.Id);
            post.Title = editPostModel.Title;
            post.Content = editPostModel.Content;
            post.UpdateDate = DateTime.Now;
            await _postRepository.Update(post);
        }

        public async Task Delete(int id)
        {
            await _postRepository.Delete(id);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
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

        public async Task Create(PostEditorModel postEditorModel, string userEmail)
        {
            var post = PostMapper.MapPostEditorModelToPost(postEditorModel);
            var user = await _userRepository.GetUserAsync(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.CreateAsync(post);
        }

        public async Task<List<PostDisplayModel>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDisplayModels = posts.Select(p => PostMapper.MapPostToPostDisplayModel(p)).ToList();
            return postDisplayModels;
        }
    }
}

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

        public async Task Create(CreatePostModel postEditorModel, string userEmail)
        {
            var post = PostMapper.MapCreatePostModelToPost(postEditorModel);
            var user = await _userRepository.GetUserAsync(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.CreateAsync(post);
        }

        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDisplayModels = posts.Select(PostMapper.MapPostToPostModel).ToList();
            return postDisplayModels;
        }

        public async Task<EditPostModel> GetPostAsync(int id)
        {
            var post = await _postRepository.GetAsync(id);
            return PostMapper.MapPostToEditPostModel(post);
        }

        public async Task EditPostAsync(EditPostModel editPostModel)
        {
            var post = await _postRepository.GetAsync(editPostModel.Id);
            post.Title = editPostModel.Title;
            post.Content = editPostModel.Content;
            post.UpdateDate = DateTime.Now;
            await _postRepository.UpdateAsync(post);
        }

        public async Task DeleteAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }
}

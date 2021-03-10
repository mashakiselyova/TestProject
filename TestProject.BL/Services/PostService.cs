using System;
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

        public async Task Create(PostEditorModel postEditorModel, string userEmail)
        {
            var post = CreatePost(postEditorModel, userEmail);
            await _postRepository.CreateAsync(post);
        }

        private Post CreatePost(PostEditorModel postEditorModel, string userEmail)
        {
            var post = PostMapper.MapPostEditorModelToPost(postEditorModel);
            var user = _userRepository.GetUser(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            return post;
        }
    }
}

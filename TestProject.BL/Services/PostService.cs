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

namespace TestProject.BL.Services
{
    public class PostService : IPostService
    {
        private IRepository<Post> _postRepository;
        private IRepository<User> _userRepository;
        private IMapper<EditPostModel, Post> _editPostMapper;
        private IMapper<PostModel, Post> _postMapper;

        public PostService(IRepository<Post> postRepository, IRepository<User> userRepository,
            IMapper<EditPostModel, Post> editPostMapper,
            IMapper<PostModel, Post> postMapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _editPostMapper = editPostMapper;
            _postMapper = postMapper;
        }

        public async Task Create(EditPostModel editPostModel, string userEmail)
        {
            var post = _editPostMapper.ToDalModel(editPostModel);
            var user = _userRepository.GetByEmail(userEmail);
            post.UserId = user.Id;
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _postRepository.Create(post);
        }

        public async Task<List<PostModel>> GetPosts(int? userId)
        {
            if (userId == null)
            {
                return (await _postRepository.Get())
                    .Select(_postMapper.ToBlModel).ToList();
            }
            return _postRepository.Get(p => p.UserId == userId.Value)
                .Select(_postMapper.ToBlModel).ToList();
        }

        public async Task<EditPostModel> Get(int id)
        {
            var post = await _postRepository.FindById(id);
            return _editPostMapper.ToBlModel(post);
        }

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

        public async Task Delete(int id)
        {
            await _postRepository.Delete(id);
        }        
    }
}

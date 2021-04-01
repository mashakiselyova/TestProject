using System;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Utils;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;

namespace TestProject.BL.Services
{
    public class CommentService : ICommentService
    {
        private IRepository<Comment> _commentRepository;
        private IRepository<User> _userRepository;
        private IMapper<CreateCommentModel, Comment> _createCommentMapper;

        public CommentService(IRepository<Comment> commentRepository,
            IRepository<User> userRepository,
            IMapper<CreateCommentModel, Comment> createCommentMapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _createCommentMapper = createCommentMapper;
        }

        public async Task Create(CreateCommentModel model, string userEmail)
        {
            var userId = _userRepository.GetByEmail(userEmail).Id;
            var comment = _createCommentMapper.ToDalModel(model);
            comment.UserId = userId;
            comment.CreateDate = DateTime.Now;
            comment.UpdateDate = comment.CreateDate;
            await _commentRepository.Create(comment);
        }
    }
}

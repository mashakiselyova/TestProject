using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using Xunit;

namespace TestProject.BL.Test.Services
{
    public class CommentServiceTests
    {
        private CommentService _commentService;
        private Mock<IRepository<Comment>> _mockCommentRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IMapper<CreateCommentModel, Comment>> _mockCreateCommentMapper;

        public CommentServiceTests()
        {
            _mockCommentRepository = new Mock<IRepository<Comment>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockCreateCommentMapper = new Mock<IMapper<CreateCommentModel, Comment>>();
            _commentService = new CommentService(_mockCommentRepository.Object,
                _mockUserRepository.Object, _mockCreateCommentMapper.Object);
        }

        [Fact]
        public async Task Should_create_comment()
        {
            var model = new CreateCommentModel { Text = "comment", PostId = 1 };
            var comment = new Comment { Text = "comment", PostId = 1 };
            var users = new List<User>() { new User { Email = "email" } };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockCreateCommentMapper.Setup(mapper => mapper.ToDalModel(model)).Returns(comment);

            await _commentService.Create(model, "email");

            _mockCommentRepository.Verify(repo => repo.Create(comment), Times.Exactly(1));
        }
    }
}

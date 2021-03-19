using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using Xunit;
using FluentAssertions;
using TestProject.BL.Exceptions;

namespace TestProject.BL.Test.Services
{
    public class PostServiceTests
    {
        private PostService _postService;
        private Mock<IRepository<Post>> _mockPostRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IMapper<EditPostModel, Post>> _mockEditPostMapper;
        private Mock<IMapper<PostModel, Post>> _mockPostMapper;

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IRepository<Post>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockEditPostMapper = new Mock<IMapper<EditPostModel, Post>>();
            _mockPostMapper = new Mock<IMapper<PostModel, Post>>();
            _postService = new PostService(_mockPostRepository.Object, _mockUserRepository.Object, _mockEditPostMapper.Object, _mockPostMapper.Object);
        }

        [Fact]
        public async Task When_called_Create_should_create_new_post()
        {
            var post = new Post();
            _mockEditPostMapper.Setup(mapper => mapper.ToDalModel(It.IsAny<EditPostModel>())).Returns(post);
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { new User() });

            await _postService.Create(new EditPostModel(), "");

            _mockPostRepository.Verify(repo => repo.Create(post), Times.Exactly(1));
        }

        [Theory]
        [MemberData(nameof(GetAllData))]
        public async Task When_called_GetAll_should_return_posts(int? userId, List<PostModel> expected)
        {
            var post = userId.HasValue ? new Post() { User = new User { Id = userId.Value } } : new Post();
            var postModel = userId.HasValue ? new PostModel { Author = new User { Id = userId.Value } } : new PostModel();
            _mockPostRepository.Setup(repo => repo.Get(It.IsAny<Func<Post, bool>>())).Returns(new List<Post>() { post });
            _mockPostRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<Post>() { post, post });
            _mockPostMapper.Setup(mapper => mapper.ToBlModel(post)).Returns(postModel);

            var result = await _postService.GetAll(userId);

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetByIdData))]
        public async Task When_called_GetById_should_return_post(int id, EditPostModel expected)
        {
            var post = new Post { Id = id };
            _mockPostRepository.Setup(repo => repo.FindById(id)).ReturnsAsync(post);
            _mockEditPostMapper.Setup(mapper => mapper.ToBlModel(post)).Returns(new EditPostModel { Id = id });

            var result = await _postService.GetById(id);

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1)]
        public async Task When_author_is_current_user_should_update_post(int userId)
        {
            var post = new Post { UserId = userId };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { new User() { Id = userId } });
            _mockPostRepository.Setup(repo => repo.FindById(It.IsAny<int>())).ReturnsAsync(post);

            await _postService.Edit(new EditPostModel(), "");

            _mockPostRepository.Verify(repo => repo.Update(post), Times.Exactly(1));
        }

        [Theory]
        [InlineData(1)]
        public async Task When_author_is_not_current_user_should_throw_exception(int userId)
        {
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { new User() { Id = userId } });
            _mockPostRepository.Setup(repo => repo.FindById(It.IsAny<int>())).ReturnsAsync(new Post { UserId = 2 });

            await Assert.ThrowsAsync<EditFailedException>(async () => await _postService.Edit(new EditPostModel(), ""));
        }

        [Theory]
        [InlineData(1)]
        public async Task When_called_Delete_should_delete_post(int postId)
        {
            await _postService.Delete(postId);

            _mockPostRepository.Verify(repo => repo.Delete(postId), Times.Exactly(1));
        }

        public static IEnumerable<object[]> GetAllData =>
            new List<object[]>
            {
                new object[] {null, new List<PostModel> { new PostModel(), new PostModel() } },
                new object[] {1, new List<PostModel> { new PostModel { Author = new User { Id = 1} } } }
            };

        public static IEnumerable<object[]> GetByIdData =>
            new List<object[]>
            {
                new object[] {1, new EditPostModel { Id = 1 } },
                new object[] {2, new EditPostModel { Id = 2 } }
            };
    }
}

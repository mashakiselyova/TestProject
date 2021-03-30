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
using System.Linq;

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
        public async Task Should_create_new_post()
        {
            var editPostModel = new EditPostModel { Title = "title", Content = "content" };
            var post = new Post { Title = "title", Content = "content" };
            var users = new List<User>() { new User { Email = "email" } };
            _mockEditPostMapper.Setup(mapper => mapper.ToDalModel(editPostModel)).Returns(post);
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());

            await _postService.Create(editPostModel, "email");

            _mockPostRepository.Verify(repo => repo.Create(post), Times.Exactly(1));
        }

        [Theory]
        [MemberData(nameof(GetAllData))]
        public async Task Should_get_filtered_posts(int? userId, List<PostModel> expected, Post post, PostModel postModel)
        {
            var user = new User { Id = 2, Email = "email" };
            var users = new List<User>() { new User { Email = "email" } };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.Get(It.IsAny<Func<Post, bool>>())).Returns(new List<Post>() { post });
            _mockPostRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<Post>() { post, post });
            _mockPostMapper.Setup(mapper => mapper.ToBlModel(post)).Returns(postModel);

            var result = await _postService.GetAll(userId, "email");

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetByIdData))]
        public async Task Should_get_specific_post(int id, EditPostModel expected)
        {
            var post = new Post { Id = id };
            var editPostModel = new EditPostModel { Id = id };
            _mockPostRepository.Setup(repo => repo.FindById(id)).ReturnsAsync(post);
            _mockEditPostMapper.Setup(mapper => mapper.ToBlModel(post)).Returns(editPostModel);

            var result = await _postService.GetById(id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task If_post_does_not_exist_should_throw_exception()
        {
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync((Post)null);

            await Assert.ThrowsAsync<PostNotFoundException>(async () => await _postService.GetById(1));
        }

        [Fact]
        public async Task When_author_is_current_user_should_update_post()
        {
            var userId = 1;
            var postId = 1;
            var editPostModel = new EditPostModel { Id = postId, Title = "title", Content = "content" };
            var post = new Post { UserId = userId, Title = "title", Content = "content" };
            var users = new List<User>() { new User() { Id = userId, Email = "email" } };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.FindById(postId)).ReturnsAsync(post);

            await _postService.Edit(editPostModel, "email");

            _mockPostRepository.Verify(repo => repo.Update(post), Times.Exactly(1));
        }

        [Fact]
        public async Task When_author_is_not_current_user_should_throw_exception()
        {
            var userId = 1;
            var postId = 1;
            var users = new List<User>() { new User() { Id = userId, Email = "email" } };
            var editPostModel = new EditPostModel { Id = postId, Title = "title", Content = "content" };
            var post = new Post { UserId = 2, Title = "title", Content = "content" };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.FindById(postId)).ReturnsAsync(post);

            await Assert.ThrowsAsync<EditFailedException>(async () => await _postService.Edit(editPostModel, "email"));
        }

        [Fact]
        public async Task When_post_is_not_valid_should_throw_exception()
        {
            var editPostModel = new EditPostModel { Title = "", Content = "" };

            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _postService.Edit(editPostModel, "email"));
        }

        [Fact]
        public async Task Should_delete_post()
        {
            var postId = 1;
            await _postService.Delete(postId);

            _mockPostRepository.Verify(repo => repo.Delete(postId), Times.Exactly(1));
        }

        public static IEnumerable<object[]> GetAllData =>
            new List<object[]>
            {
                new object[]
                {
                    null,
                    new List<PostModel>
                    {
                        new PostModel() { Ratings = new List<Rating>() { new Rating() { UserId = 2 } } },
                        new PostModel() { Ratings = new List<Rating>() { new Rating() { UserId = 2 } } }
                    },
                    new Post() { Ratings = new List<Rating>() { new Rating() { UserId = 2} } },
                    new PostModel() { Ratings = new List<Rating>() { new Rating() { UserId = 2} } }
                },
                new object[]
                {
                    1,
                    new List<PostModel>
                    {
                        new PostModel { Author = new Author { Id = 1 } , Ratings = new List<Rating>() { new Rating() { UserId = 2} } }
                    },
                    new Post() { User = new User { Id = 1 }, Ratings = new List<Rating>() { new Rating() { UserId = 2 } } },
                    new PostModel { Author = new Author { Id = 1 }, Ratings = new List<Rating>() { new Rating() { UserId = 2} } }
                }
            };

        public static IEnumerable<object[]> GetByIdData =>
            new List<object[]>
            {
                new object[] {1, new EditPostModel { Id = 1 } },
                new object[] {2, new EditPostModel { Id = 2 } }
            };
    }
}

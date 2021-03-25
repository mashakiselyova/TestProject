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
using TestProject.BL.Exceptions;
using System.Linq;
using TestProject.BL.Enums;
using TestProject.DAL.Enums;
using FluentAssertions;

namespace TestProject.BL.Test.Services
{
    public class RatingServiceTests
    {
        private RatingService _ratingService;
        private Mock<IRepository<Rating>> _mockRatingRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<Post>> _mockPostRepository;
        private Mock<IMapper<RatingModel, Rating>> _mockRatingMapper;

        public RatingServiceTests()
        {
            _mockRatingRepository = new Mock<IRepository<Rating>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockPostRepository = new Mock<IRepository<Post>>();
            _mockRatingMapper = new Mock<IMapper<RatingModel, Rating>>();
            _ratingService = new RatingService(_mockRatingRepository.Object,
                _mockUserRepository.Object, _mockPostRepository.Object, _mockRatingMapper.Object);
        }

        [Fact]
        public async Task Should_set_rating()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingOption.Plus };
            var rating = new Rating();
            var user = new User { Id = 2, Email = "email" };
            var users = new List<User>() { user };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(new List<Rating>());
            _mockRatingMapper.Setup(mapper => mapper.ToDalModel(ratingModel)).Returns(rating);

            await _ratingService.Set(ratingModel, "email");

            _mockRatingRepository.Verify(repo => repo.Create(rating), Times.Exactly(1));
        }

        [Fact]
        public async Task If_current_user_is_author_should_throw_exception()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingOption.Plus };
            var rating = new Rating();
            var user = new User { Id = 1, Email = "email" };
            var users = new List<User>() { user };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).
                Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });

            await Assert.ThrowsAsync<RatingFailedException>(async () => await _ratingService.Set(ratingModel, "email"));
        }

        [Theory]
        [MemberData(nameof(RatingData))]
        public void Should_get_updated_rating(int postId, UpdateRatingModel expected)
        {
            var user = new User { Id = 1, Email = "email" };
            var users = new List<User>() { user };
            var posts = new List<Post>() { new Post { Id = postId } };
            var ratings = new List<Rating>() { new Rating { PostId = postId, UserId = 1, Value = RatingValue.Plus } };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).
                Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.Get(It.IsAny<Func<Post, bool>>())).
                Returns((Func<Post, bool> predicate) => posts.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).
                Returns((Func<Rating, bool> predicate) => ratings.Where(predicate).ToList());

            var result = _ratingService.GetUpdatedRating(1, "email");

            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> RatingData =>
            new List<object[]>
            {
                new object[] {1, new UpdateRatingModel 
                { 
                    TotalRating = 1,
                    RatingByCurrentUser = RatingOption.Plus 
                } }
            };
    }
}

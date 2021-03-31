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
using FluentAssertions;
using TestProject.Enums;

namespace TestProject.BL.Test.Services
{
    public class RatingServiceTests
    {
        private RatingService _ratingService;
        private Mock<IRepository<PostRating>> _mockRatingRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<Post>> _mockPostRepository;
        private Mock<IMapper<RatingModel, PostRating>> _mockRatingMapper;

        public RatingServiceTests()
        {
            _mockRatingRepository = new Mock<IRepository<PostRating>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockPostRepository = new Mock<IRepository<Post>>();
            _mockRatingMapper = new Mock<IMapper<RatingModel, PostRating>>();
            _ratingService = new RatingService(_mockRatingRepository.Object,
                _mockUserRepository.Object, _mockPostRepository.Object, _mockRatingMapper.Object);
        }

        [Fact]
        public async Task If_rating_is_not_set_should_create_rating()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingButtonPosition.ThumbsUp };
            var rating = new PostRating();
            var ratings = new List<PostRating>() { rating };
            var users = new List<User>() { new User { Id = 2, Email = "email" } };
            Setup(users, ratings);
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });
            _mockRatingMapper.Setup(mapper => mapper.ToDalModel(ratingModel)).Returns(rating);

            await _ratingService.Set(ratingModel, "email");

            _mockRatingRepository.Verify(repo => repo.Create(rating), Times.Exactly(1));
        }

        [Fact]
        public async Task If_rating_is_cancelled_should_delete_rating()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingButtonPosition.ThumbsUp };
            var rating = new PostRating { Id = 1, PostId = 1, UserId = 2, Value = RatingValue.Plus };
            var ratings = new List<PostRating>() { rating };
            var users = new List<User>() { new User { Id = 2, Email = "email" } };
            Setup(users, ratings);
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });

            await _ratingService.Set(ratingModel, "email");

            _mockRatingRepository.Verify(repo => repo.Delete(1), Times.Exactly(1));
        }

        [Fact]
        public async Task If_rating_has_changed_should_update_rating()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingButtonPosition.ThumbsUp };
            var rating = new PostRating { Id = 1, PostId = 1, UserId = 2, Value = RatingValue.Minus };
            var ratings = new List<PostRating>() { rating };
            var users = new List<User>() { new User { Id = 2, Email = "email" } };
            Setup(users, ratings);
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });

            await _ratingService.Set(ratingModel, "email");

            var newRating = rating;
            rating.Value = RatingValue.Plus;
            _mockRatingRepository.Verify(repo => repo.Update(newRating), Times.Exactly(1));
        }

        [Fact]
        public async Task If_current_user_is_author_should_throw_exception()
        {
            var ratingModel = new RatingModel { PostId = 1, Value = RatingButtonPosition.ThumbsUp };
            var rating = new PostRating();
            var users = new List<User>() { new User { Id = 1, Email = "email" } };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).
                Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockPostRepository.Setup(repo => repo.FindById(1)).ReturnsAsync(new Post { UserId = 1 });

            await Assert.ThrowsAsync<RatingFailedException>(async () => await _ratingService.Set(ratingModel, "email"));
        }

        [Theory]
        [MemberData(nameof(RatingData))]
        public void Should_get_updated_rating(int postId, UpdateRatingModel expected)
        {
            var users = new List<User>() { new User { Id = 1, Email = "email" } };
            var posts = new List<Post>() { new Post { Id = postId } };
            var ratings = new List<PostRating>() { new PostRating { PostId = postId, UserId = 1, Value = RatingValue.Plus } };
            Setup(users, ratings);
            _mockPostRepository.Setup(repo => repo.Get(It.IsAny<Func<Post, bool>>())).
                Returns((Func<Post, bool> predicate) => posts.Where(predicate).ToList());

            var result = _ratingService.GetUpdatedRating(1, "email");

            result.Should().BeEquivalentTo(expected);
        }

        private void Setup(List<User> users, List<PostRating> ratings)
        {
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).
                Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<PostRating, bool>>())).
                Returns((Func<PostRating, bool> predicate) => ratings.Where(predicate).ToList());
        }

        public static IEnumerable<object[]> RatingData =>
            new List<object[]>
            {
                new object[] {1, new UpdateRatingModel 
                { 
                    TotalRating = 1,
                    RatingByCurrentUser = RatingValue.Plus 
                } }
            };
    }
}

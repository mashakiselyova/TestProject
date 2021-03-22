using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.DAL.Enums;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using Xunit;

namespace TestProject.BL.Test.Services
{
    public class RatingServiceTests
    {
        private RatingService _ratingService;
        private Mock<IRepository<Rating>> _mockRatingRepository;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IMapper<RatingModel, Rating>> _mockRatingMapper;

        public RatingServiceTests()
        {
            _mockRatingRepository = new Mock<IRepository<Rating>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockRatingMapper = new Mock<IMapper<RatingModel, Rating>>();
            _ratingService = new RatingService(_mockRatingRepository.Object, 
                _mockUserRepository.Object, _mockRatingMapper.Object);
        }

        [Fact]
        public async Task Should_set_rating()
        {
            var ratingModel = new RatingModel();
            var rating = new Rating();
            var user = new User();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { user });
            _mockRatingMapper.Setup(mapper => mapper.ToDalModel(ratingModel)).Returns(rating);

            await _ratingService.Set(ratingModel, "");

            _mockRatingRepository.Verify(repo => repo.Create(rating), Times.Exactly(1));
        }

        [Fact]
        public void When_rated_should_return_true()
        {
            var rating = new Rating();
            var user = new User();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { user });
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(new List<Rating>() { rating });

            var result = _ratingService.CheckIfRated(1, "");

            Assert.True(result);
        }

        [Fact]
        public void When_not_rated_should_return_false()
        {
            var user = new User();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>() { user });
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(new List<Rating>());

            var result = _ratingService.CheckIfRated(1, "");

            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(GetRatingData))]
        public void Should_get_rating(int expected, List<Rating> ratings)
        {
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(ratings);

            var result = _ratingService.Get(1);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetRatingData =>
            new List<object[]>
            {
                new object[] { 1, new List<Rating>()
                {
                    new Rating { Value = RatingValue.Plus },
                    new Rating { Value = RatingValue.Plus },
                    new Rating { Value = RatingValue.Minus }
                } },
                new object[] { 2, new List<Rating>()
                {
                    new Rating { Value = RatingValue.Plus },
                    new Rating { Value = RatingValue.Plus }
                } },
                new object[] { -1, new List<Rating>()
                {
                    new Rating { Value = RatingValue.Minus }
                } }
            };
    }
}

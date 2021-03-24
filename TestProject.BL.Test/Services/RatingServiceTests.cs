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
using TestProject.BL.Exceptions;
using System.Linq;

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
            var ratingModel = new RatingModel { AuthorId = 1, Value = RatingValue.Plus };
            var rating = new Rating();
            var user = new User { Id = 2, Email = "email" };
            var users = new List<User>() { user };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(new List<Rating>());
            _mockRatingMapper.Setup(mapper => mapper.ToDalModel(ratingModel)).Returns(rating);

            await _ratingService.Set(ratingModel, "email");

            _mockRatingRepository.Verify(repo => repo.Create(rating), Times.Exactly(1));
        }

        [Fact]
        public async Task If_current_user_is_author_should_throw_exception()
        {
            var ratingModel = new RatingModel { AuthorId = 1, Value = RatingValue.Plus };
            var rating = new Rating();
            var user = new User { Id = 1, Email = "email" }; 
            var users = new List<User>() { user };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).
                Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());

            await Assert.ThrowsAsync<RatingFailedException>(async () => await _ratingService.Set(ratingModel, "email"));
        }
    }
}

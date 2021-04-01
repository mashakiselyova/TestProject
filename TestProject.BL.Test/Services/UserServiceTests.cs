using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Exceptions;
using TestProject.BL.Mappers;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using TestProject.Enums;
using Xunit;

namespace TestProject.BL.Test.Services
{
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<PostRating>> _mockRatingRepository;
        private Mock<IMapper<UserLoginModel, User>> _mockUserLoginMapper;
        private Mock<IMapper<UserProfile, User>> _mockUserProfileMapper;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockRatingRepository = new Mock<IRepository<PostRating>>();
            _mockUserLoginMapper = new Mock<IMapper<UserLoginModel, User>>();
            _mockUserProfileMapper = new Mock<IMapper<UserProfile, User>>();
            _userService = new UserService(_mockUserRepository.Object, 
                _mockRatingRepository.Object, 
                _mockUserLoginMapper.Object, 
                _mockUserProfileMapper.Object);
        }

        [Fact]
        public async Task When_user_does_not_exists_should_create_user()
        {
            var userLoginModel = new UserLoginModel();
            var user = new User();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            _mockUserLoginMapper.Setup(mapper => mapper.ToDalModel(userLoginModel)).Returns(user);

            await _userService.AddOrUpdate(userLoginModel);

            _mockUserRepository.Verify(repo => repo.Create(user), Times.Exactly(1));
        }

        [Fact]
        public async Task When_user_has_changed_should_update_user()
        {
            var userLoginModel = new UserLoginModel { FirstName = "Bla" };
            var user = new User();
            var users = new List<User>() { user };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockUserLoginMapper.Setup(mapper => mapper.ToDalModel(userLoginModel)).Returns(user);

            await _userService.AddOrUpdate(userLoginModel);

            _mockUserRepository.Verify(repo => repo.Update(user), Times.Exactly(1));
        }

        [Theory]
        [MemberData(nameof(GetProfileData))]
        public void Should_get_current_user_profile(string email, UserProfile expected)
        {
            var user = new User { Id = 1, Email = email };
            var users = new List<User>() { user };
            var rating = new PostRating() { Value = RatingValue.Plus, Post = new Post { UserId = 1 } };
            var ratings = new List<PostRating> { rating };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<PostRating, bool>>()))
                .Returns((Func<PostRating, bool> predicate) => ratings.Where(predicate).ToList());
            _mockUserProfileMapper.Setup(mapper => mapper.ToBlModel(user)).Returns(new UserProfile { Id = 1, Email = email });

            var result = _userService.GetByEmail(email);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void If_current_user_is_not_found_should_throw_exception()
        {
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());

            Assert.Throws<UserNotFoundException>(() => _userService.GetByEmail("email"));
        }

        [Theory]
        [MemberData(nameof(GetUser))]
        public void Should_get__user_profile(int userId, UserProfile expected)
        {
            var user = new User { Id = userId };
            var users = new List<User>() { user };
            var rating = new PostRating() { Value = RatingValue.Plus, Post = new Post { UserId = 1 } };
            var ratings = new List<PostRating> { rating };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<PostRating, bool>>()))
                .Returns((Func<PostRating, bool> predicate) => ratings.Where(predicate).ToList());
            _mockUserProfileMapper.Setup(mapper => mapper.ToBlModel(user)).Returns(new UserProfile { Id = userId });

            var result = _userService.Get(userId);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void If_user_does_not_exist_should_throw_exception()
        {
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());

            Assert.Throws<UserNotFoundException>(() => _userService.Get(1));
        }

        public static IEnumerable<object[]> GetProfileData =>
            new List<object[]>
            {
                new object[] { "bla@bla.com", new UserProfile { Id = 1, Email = "bla@bla.com", Rating = 1 } }
            };

        public static IEnumerable<object[]> GetUser =>
            new List<object[]>
            {
                new object[] { 1, new UserProfile { Id = 1, Rating = 1 } }
            };
    }
}

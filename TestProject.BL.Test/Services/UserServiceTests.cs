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
        public void Should_get_user_profile(string email, UserProfile expected)
        {
            var user = new User { Email = email };
            var users = new List<User>() { user };
            var rating = new PostRating() { Value = RatingValue.Plus };
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<PostRating, bool>>())).Returns(new List<PostRating> { rating });
            _mockUserProfileMapper.Setup(mapper => mapper.ToBlModel(user)).Returns(new UserProfile { Email = email });

            var result = _userService.GetProfile(email);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void If_user_does_not_exist_should_throw_exception()
        {
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>()))
                .Returns((Func<User, bool> predicate) => users.Where(predicate).ToList());

            Assert.Throws<UserNotFoundException>(() => _userService.GetProfile("email"));
        }

        public static IEnumerable<object[]> GetProfileData =>
            new List<object[]>
            {
                new object[] { "bla@bla.com", new UserProfile { Email = "bla@bla.com", Rating = 1 } }
            };
    }
}

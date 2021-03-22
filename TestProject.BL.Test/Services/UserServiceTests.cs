using FluentAssertions;
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

namespace TestProject.BL.Test.Services
{
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<Rating>> _mockRatingRepository;
        private Mock<IMapper<UserLoginModel, User>> _mockUserLoginMapper;
        private Mock<IMapper<UserProfile, User>> _mockUserProfileMapper;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockRatingRepository = new Mock<IRepository<Rating>>();
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
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            _mockUserLoginMapper.Setup(mapper => mapper.ToDalModel(userLoginModel)).Returns(user);

            await _userService.AddOrUpdate(userLoginModel);

            _mockUserRepository.Verify(repo => repo.Update(user), Times.Exactly(1));
        }

        [Theory]
        [MemberData(nameof(GetProfileData))]
        public void Should_get_user_profile(string email, UserProfile expected)
        {
            var user = new User { Email = email };
            var rating = new Rating();
            _mockUserRepository.Setup(repo => repo.Get(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            _mockRatingRepository.Setup(repo => repo.Get(It.IsAny<Func<Rating, bool>>())).Returns(new List<Rating> { rating });
            _mockUserProfileMapper.Setup(mapper => mapper.ToBlModel(user)).Returns(new UserProfile { Email = email });

            var result = _userService.GetProfile(email);

            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetProfileData =>
            new List<object[]>
            {
                new object[] { "bla@bla.com", new UserProfile { Email = "bla@bla.com", Rating = 1 } }
            };
    }
}

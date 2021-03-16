using TestProject.DAL.Repositories;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Mappers;
using TestProject.DAL.Models;

namespace TestProject.BL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper<UserLoginModel, User> _userLoginMapper;
        private IMapper<UserProfile, User> _userProfileMapper;

        public UserService(IUserRepository repository, 
            IMapper<UserLoginModel, User> userLoginMapper,
            IMapper<UserProfile, User> userProfileMapper)
        {
            _userRepository = repository;
            _userLoginMapper = userLoginMapper;
            _userProfileMapper = userProfileMapper;
        }
        public async Task AddOrUpdateUserAsync(UserLoginModel userLoginModel)
        {
            if (_userRepository.UserExists(userLoginModel.Email))
            {
                await UpdateAsync(userLoginModel);
            }
            else
            {
                await Create(userLoginModel);
            }
        }

        public async Task<UserProfile> GetUserProfileAsync(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return _userProfileMapper.ToBlModel(user);
        }
        
        private bool UserHasChanged(UserLoginModel userLoginModel, User user)
        {
            return !(userLoginModel.FirstName == user.FirstName && userLoginModel.LastName == user.LastName);
        }

        private async Task Create(UserLoginModel userLoginModel)
        {
            var user = _userLoginMapper.ToDalModel(userLoginModel);
            await _userRepository.Create(user);
        }

        private async Task UpdateAsync(UserLoginModel userLoginModel)
        {
            var user = await _userRepository.GetUserByEmail(userLoginModel.Email);
            if (UserHasChanged(userLoginModel, user))
            {
                var newUser = _userLoginMapper.ToDalModel(userLoginModel);
                newUser.Id = user.Id;
                await _userRepository.Update(newUser);
            }
        }
    }
}

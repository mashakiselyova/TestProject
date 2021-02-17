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

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task AddOrUpdateUserAsync(UserLoginModel userLoginModel)
        {
            if (_userRepository.UserExists(userLoginModel.Email))
            {
                var user = _userRepository.GetUser(userLoginModel.Email);
                if (UserHasChanged(userLoginModel, user))
                {
                    await UpdateAsync(userLoginModel, user);
                }
            }
            else
            {
                await Create(userLoginModel);
            }
        }        

        public UserProfile GetUserProfile(string email)
        {
            var user = _userRepository.GetUser(email);
            return UserMapper.MapUserToUserProfile(user);
        }
        
        private bool UserHasChanged(UserLoginModel userLoginModel, User user)
        {
            return !(userLoginModel.FirstName == user.FirstName && userLoginModel.LastName == user.LastName);
        }

        private async Task Create(UserLoginModel userLoginModel)
        {
            var user = UserMapper.MapUserLoginModelToUser(userLoginModel);
            await _userRepository.CreateAsync(user);
        }

        private async Task UpdateAsync(UserLoginModel userLoginModel, User user)
        {
            var newUser = UserMapper.MapUserLoginModelToUser(userLoginModel);
            newUser.Id = user.Id;
            await _userRepository.UpdateAsync(newUser);
        }
    }
}

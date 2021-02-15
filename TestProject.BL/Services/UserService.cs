using TestProject.DAL.Repositories;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Mappers;
using TestProject.DAL.Models;

namespace TestProject.BL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepo;

        public UserService(IUserRepository repo)
        {
            _userRepo = repo;
        }
        public async Task<UserLoginModel> AddOrUpdateUserAsync(UserLoginModel userLoginModel)
        {
            if (_userRepo.UserExists(userLoginModel.Email))
            {
                var user = _userRepo.GetUser(userLoginModel.Email);
                var userChanged = CheckUserForChanges(userLoginModel, user);
                if (userChanged)
                {
                    var newUser = UserMapper.MapUserLoginModelToUser(userLoginModel);
                    newUser.Id = user.Id;
                    _userRepo.Update(newUser);
                }
            }
            else
            {
                var user = UserMapper.MapUserLoginModelToUser(userLoginModel);
                await _userRepo.CreateAsync(user);
            }
            return userLoginModel;
        }

        public UserProfile GetUserProfile(string email)
        {
            var user = _userRepo.GetUser(email);
            return UserMapper.MapUserToUserProfile(user);
        }
        
        private bool CheckUserForChanges(UserLoginModel userLoginModel, User user)
        {
            return !(userLoginModel.FirstName == user.FirstName && userLoginModel.LastName == user.LastName);
        }
    }
}

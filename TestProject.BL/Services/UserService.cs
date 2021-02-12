using TestProject.DAL.Models;
using TestProject.DAL.Repositories;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<UserLoginModel> AddUserAsync(UserLoginModel userLoginModel)
        {
            if (!UserExists(userLoginModel.Email))
            {
                var user = new User
                {
                    FirstName = userLoginModel.FirstName,
                    LastName = userLoginModel.LastName,
                    Email = userLoginModel.Email
                };
                await _repo.CreateAsync(user);
            }
            return userLoginModel;
        }

        public bool UserExists(string email)
        {
            return _repo.UserExists(email);
        }
    }
}

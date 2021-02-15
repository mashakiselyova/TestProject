using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IUserService
    {
        Task<UserLoginModel> AddOrUpdateUserAsync(UserLoginModel userLoginModel);
        UserProfile GetUserProfile(string email);
    }
}

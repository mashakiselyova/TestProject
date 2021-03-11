using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IUserService
    {
        Task AddOrUpdateUserAsync(UserLoginModel userLoginModel);
        Task<UserProfile> GetUserProfileAsync(string email);
    }
}

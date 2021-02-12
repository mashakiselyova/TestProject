using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IUserService
    {
        Task<UserLoginModel> AddUserAsync(UserLoginModel userLoginModel);
    }
}

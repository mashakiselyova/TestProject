using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IUserService
    {
        Task AddOrUpdate(UserLoginModel userLoginModel);
        UserProfile GetProfile(string email);
    }
}

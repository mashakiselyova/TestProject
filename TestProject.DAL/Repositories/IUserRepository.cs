using System.Threading.Tasks;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserExists(string email);
        Task<User> GetUserAsync(string email);
    }
}

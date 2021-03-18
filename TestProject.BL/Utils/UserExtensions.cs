using System.Linq;
using TestProject.DAL.Models;
using TestProject.DAL.Repositories;

namespace TestProject.BL.Utils
{
    public static class UserExtensions
    {
        public static User GetByEmail(this IRepository<User> repository, string email)
        {
            return repository.Get(u => u.Email == email).First();
        }
    }
}

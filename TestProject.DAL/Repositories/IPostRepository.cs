using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetUserPosts(int id);
    }
}

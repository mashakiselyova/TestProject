using System.Collections.Generic;
using TestProject.DAL.Models;

namespace TestProject.DAL.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        List<Post> GetAllPosts();
    }
}

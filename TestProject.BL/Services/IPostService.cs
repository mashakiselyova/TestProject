using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(EditPostModel editPostModel, string userEmail);
        Task<List<PostModel>> GetPosts(int? userId);
        Task<EditPostModel> Get(int id);
        Task Edit(EditPostModel editPostModel);
        Task Delete(int id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(CreatePostModel postEditorModel, string userEmail);
        Task<List<PostModel>> GetAll();
        Task<List<PostModel>> GetUserPosts(int id);
        Task<EditPostModel> Get(int id);
        Task Edit(EditPostModel editPostModel);
        Task Delete(int id);
    }
}
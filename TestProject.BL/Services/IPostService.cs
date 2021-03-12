using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(CreatePostModel postEditorModel, string userEmail);
        Task<List<PostModel>> GetAllPostsAsync();
        Task<EditPostModel> GetPostAsync(int id);
        Task EditPostAsync(EditPostModel editPostModel);
        Task DeleteAsync(int id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(PostEditorModel postEditorModel, string userEmail);
        Task<List<PostDisplayModel>> GetAllPostsAsync();
    }
}
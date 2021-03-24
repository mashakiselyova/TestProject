using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(EditPostModel editPostModel, string userEmail);
        Task<List<PostModel>> GetAll(int? userId, string v);
        Task<EditPostModel> GetById(int id);
        Task Edit(EditPostModel editPostModel, string userEmail);
        Task Delete(int id);
    }
}
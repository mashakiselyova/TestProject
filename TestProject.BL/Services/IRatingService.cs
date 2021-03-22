using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IRatingService
    {
        Task Set(RatingModel ratingModel, string email);
        bool CheckIfRated(int postId, string email);
        int Get(int postId);
    }
}

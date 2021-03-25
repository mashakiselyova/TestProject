using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IRatingService
    {
        Task Set(RatingModel ratingModel, string email);
        UpdateRatingModel GetUpdatedRating(int postId, string email);
    }
}

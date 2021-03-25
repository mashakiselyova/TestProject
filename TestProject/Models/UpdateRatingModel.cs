using TestProject.BL.Enums;

namespace TestProject.Models
{
    public class UpdateRatingModel
    {
        public int TotalRating { get; set; }
        public RatingOption RatingByCurrentUser { get; set; }
    }
}

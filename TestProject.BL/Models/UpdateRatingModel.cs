using TestProject.Enums;

namespace TestProject.BL.Models
{
    public class UpdateRatingModel
    {
        public int TotalRating { get; set; }
        public RatingValue RatingByCurrentUser { get; set; }
    }
}

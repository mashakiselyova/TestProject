using TestProject.Enums;

namespace TestProject.Models
{
    public class UpdateRatingModel
    {
        public int TotalRating { get; set; }
        public RatingValue RatingByCurrentUser { get; set; }
    }
}

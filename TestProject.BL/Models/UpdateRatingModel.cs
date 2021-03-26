using TestProject.BL.Enums;

namespace TestProject.BL.Models
{
    public class UpdateRatingModel
    {
        public int TotalRating { get; set; }
        public RatingButtonPosition RatingByCurrentUser { get; set; }
    }
}

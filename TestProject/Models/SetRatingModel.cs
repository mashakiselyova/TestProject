using TestProject.DAL.Enums;

namespace TestProject.Models
{
    public class SetRatingModel
    {
        public RatingValue Value { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
    }
}

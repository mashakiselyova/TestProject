using TestProject.DAL.Enums;

namespace TestProject.BL.Models
{
    public class RatingModel
    {
        public RatingValue Value { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
    }
}

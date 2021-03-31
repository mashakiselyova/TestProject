using TestProject.Enums;

namespace TestProject.DAL.Models
{
    public class CommentRating
    {
        public int Id { get; set; }
        public RatingValue Value { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

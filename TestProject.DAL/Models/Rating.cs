using TestProject.DAL.Enums;

namespace TestProject.DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public RatingValue Value { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }    
}

using System.Collections.Generic;

namespace TestProject.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PostRating> PostRatings { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

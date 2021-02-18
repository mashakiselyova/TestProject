using System;

namespace TestProject.DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

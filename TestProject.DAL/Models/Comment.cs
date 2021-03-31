using System;
using System.Collections.Generic;

namespace TestProject.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int? ParentId { get; set; }
        public List<CommentRating> CommentRatings { get; set; }
    }
}

using System;
using TestProject.DAL.Models;

namespace TestProject.BL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public User Author { get; set; }
    }
}

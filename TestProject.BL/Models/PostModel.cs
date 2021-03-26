using System;
using System.Collections.Generic;
using TestProject.BL.Enums;
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
        public Author Author { get; set; }
        public RatingButtonPosition SelectedRating { get; set; }
        public int TotalRating { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}

using System;
using TestProject.BL.Enums;
using TestProject.BL.Models;

namespace TestProject.Models
{
    public class PostDisplayModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Author Author { get; set; }
        public RatingOption SelectedRating { get; set; }
        public int TotalRating { get; set; }
    }
}

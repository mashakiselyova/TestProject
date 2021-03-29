using System;
using TestProject.Enums;

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
        public RatingValue SelectedRating { get; set; }
        public int TotalRating { get; set; }
    }
}

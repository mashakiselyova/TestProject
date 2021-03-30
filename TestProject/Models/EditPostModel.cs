using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class EditPostModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}

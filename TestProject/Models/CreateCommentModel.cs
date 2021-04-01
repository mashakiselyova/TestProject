namespace TestProject.Models
{
    public class CreateCommentModel
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public int? ParentId { get; set; }
    }
}

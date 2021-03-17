using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class EditPostMapper : IMapper<EditPostModel, Post>
    {
        public EditPostModel ToBlModel(Post post)
        {
            return new EditPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }

        public Post ToDalModel(EditPostModel post)
        {
            return new Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }
    }
}

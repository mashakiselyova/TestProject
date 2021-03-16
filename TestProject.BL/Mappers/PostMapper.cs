using TestProject.DAL.Models;
using TestProject.BL.Models;

namespace TestProject.BL.Mappers
{
    public class PostMapper : IMapper<PostModel, Post>
    {
        public PostModel ToBlModel(Post post)
        {
            return new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = post.User
            };
        }

        public Post ToDalModel(PostModel post)
        {
            throw new System.NotImplementedException();
        }
    }
}

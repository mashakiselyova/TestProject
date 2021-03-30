using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class RichPostMapper : IMapper<RichPostModel, Post>
    {
        public RichPostModel ToBlModel(Post post)
        {
            return new RichPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = new Author
                {
                    Id = post.User.Id,
                    FirstName = post.User.FirstName,
                    LastName = post.User.LastName,
                    Email = post.User.Email
                }
            };
        }

        public Post ToDalModel(RichPostModel post)
        {
            return new Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                User = new User
                {
                    Id = post.Author.Id,
                    FirstName = post.Author.FirstName,
                    LastName = post.Author.LastName,
                    Email = post.Author.Email
                }
            };
        }
    }
}

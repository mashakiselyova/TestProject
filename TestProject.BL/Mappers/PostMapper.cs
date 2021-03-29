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
                Author = new Author
                {
                    Id = post.User.Id,
                    FirstName = post.User.FirstName,
                    LastName = post.User.LastName,
                    Email = post.User.Email
                },
                Ratings = post.Ratings
            };
        }

        public Post ToDalModel(PostModel post)
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

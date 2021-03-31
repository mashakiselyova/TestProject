using TestProject.DAL.Models;
using TestProject.BL.Models;

namespace TestProject.BL.Mappers
{
    public class PostMapper : IMapper<PostModel, Post>
    {
        private IMapper<Author, User> _userAuthorMapper;

        public PostMapper(IMapper<Author, User> userAuthorMapper)
        {
            _userAuthorMapper = userAuthorMapper;
        }

        public PostModel ToBlModel(Post post)
        {
            return new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _userAuthorMapper.ToBlModel(post.User),
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
                User = _userAuthorMapper.ToDalModel(post.Author)
            };
        }
    }
}

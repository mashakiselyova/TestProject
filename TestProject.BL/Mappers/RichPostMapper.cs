using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class RichPostMapper : IMapper<RichPostModel, Post>
    {
        private IMapper<Author, User> _userAuthorMapper;

        public RichPostMapper(IMapper<Author, User> userAuthorMapper)
        {
            _userAuthorMapper = userAuthorMapper;
        }

        public RichPostModel ToBlModel(Post post)
        {
            return new RichPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _userAuthorMapper.ToBlModel(post.User)
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
                User = _userAuthorMapper.ToDalModel(post.Author)
            };
        }
    }
}

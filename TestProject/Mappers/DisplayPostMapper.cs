using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class DisplayPostMapper : IMapper<PostDisplayModel, PostModel>
    {
        private IMapper<Models.Author, BL.Models.Author> _authorMapper;

        public DisplayPostMapper(IMapper<Models.Author, BL.Models.Author> authorMapper)
        {
            _authorMapper = authorMapper;
        }

        public PostDisplayModel ToWebModel(PostModel post)
        {
            return new PostDisplayModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _authorMapper.ToWebModel(post.Author),
                SelectedRating = post.SelectedRating,
                TotalRating = post.TotalRating
            };
        }

        public PostModel ToBlModel(PostDisplayModel post)
        {
            return new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _authorMapper.ToBlModel(post.Author),
                SelectedRating = post.SelectedRating,
                TotalRating = post.TotalRating
            };
        }        
    }    
}

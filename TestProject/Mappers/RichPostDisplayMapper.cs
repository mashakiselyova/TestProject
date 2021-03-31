using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class RichPostDisplayMapper : IMapper<RichPostDisplayModel, RichPostModel>
    {
        private IMapper<Models.Author, BL.Models.Author> _authorMapper;

        public RichPostDisplayMapper(IMapper<Models.Author, BL.Models.Author> authorMapper)
        {
            _authorMapper = authorMapper;
        }

        public RichPostModel ToBlModel(RichPostDisplayModel post)
        {
            return new RichPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _authorMapper.ToBlModel(post.Author),
                TotalRating = post.TotalRating,
                SelectedRating = post.SelectedRating
            };
        }

        public RichPostDisplayModel ToWebModel(RichPostModel post)
        {
            return new RichPostDisplayModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = _authorMapper.ToWebModel(post.Author),
                TotalRating = post.TotalRating,
                SelectedRating = post.SelectedRating
            };
        }
    }
}

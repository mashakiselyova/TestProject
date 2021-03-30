using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class RichPostDisplayMapper : IMapper<RichPostDisplayModel, RichPostModel>
    {
        public RichPostModel ToBlModel(RichPostDisplayModel post)
        {
            return new RichPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = new BL.Models.Author
                {
                    Id = post.Author.Id,
                    FirstName = post.Author.FirstName,
                    LastName = post.Author.LastName,
                    Email = post.Author.Email
                },
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
                Author = new Models.Author
                {
                    Id = post.Author.Id,
                    FirstName = post.Author.FirstName,
                    LastName = post.Author.LastName,
                    Email = post.Author.Email
                },
                TotalRating = post.TotalRating,
                SelectedRating = post.SelectedRating
            };
        }
    }
}

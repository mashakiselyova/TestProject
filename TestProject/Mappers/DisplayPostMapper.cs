using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class DisplayPostMapper : IMapper<PostDisplayModel, PostModel>
    {
        public PostDisplayModel ToWebModel(PostModel post)
        {
            return new PostDisplayModel
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
                Author = new BL.Models.Author
                {
                    Id = post.Author.Id,
                    FirstName = post.Author.FirstName,
                    LastName = post.Author.LastName,
                    Email = post.Author.Email
                },
                SelectedRating = post.SelectedRating,
                TotalRating = post.TotalRating
            };
        }        
    }    
}

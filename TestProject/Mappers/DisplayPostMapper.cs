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
                Author = post.Author
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
                Author = post.Author
            };
        }        
    }    
}

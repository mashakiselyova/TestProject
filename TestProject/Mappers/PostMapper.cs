using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public static class PostMapper
    {
        public static PostDisplayModel MapPostModelToPostModel(PostModel post)
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
    }    
}

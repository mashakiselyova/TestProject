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

        public static BL.Models.EditPostModel MapEditPostModelWebToBl(Models.EditPostModel post)
        {
            return new BL.Models.EditPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }

        public static Models.EditPostModel MapEditPostModelDlToWeb(BL.Models.EditPostModel post)
        {
            return new Models.EditPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }
    }    
}

namespace TestProject.Mappers
{
    public class EditPostMapper : IMapper<Models.EditPostModel, BL.Models.EditPostModel>
    {
        public Models.EditPostModel ToWebModel(BL.Models.EditPostModel post)
        {
            return new Models.EditPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }

        public BL.Models.EditPostModel ToBlModel(Models.EditPostModel post)
        {
            return new BL.Models.EditPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
        }
    }
}

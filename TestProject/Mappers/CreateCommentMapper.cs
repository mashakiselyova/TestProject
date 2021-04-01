namespace TestProject.Mappers
{
    public class CreateCommentMapper : IMapper<Models.CreateCommentModel, BL.Models.CreateCommentModel>
    {
        public BL.Models.CreateCommentModel ToBlModel(Models.CreateCommentModel comment)
        {
            return new BL.Models.CreateCommentModel
            {
                Text = comment.Text,
                PostId = comment.PostId,
                ParentId = comment.ParentId
            };
        }

        public Models.CreateCommentModel ToWebModel(BL.Models.CreateCommentModel comment)
        {
            return new Models.CreateCommentModel
            {
                Text = comment.Text,
                PostId = comment.PostId,
                ParentId = comment.ParentId
            };
        }
    }
}

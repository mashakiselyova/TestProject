using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class CreateCommentMapper : IMapper<CreateCommentModel, Comment>
    {
        public CreateCommentModel ToBlModel(Comment comment)
        {
            return new CreateCommentModel
            {
                Text = comment.Text,
                PostId = comment.PostId,
                ParentId = comment.ParentId
            };
        }

        public Comment ToDalModel(CreateCommentModel comment)
        {
            return new Comment
            {
                Text = comment.Text,
                PostId = comment.PostId,
                ParentId = comment.ParentId
            };
        }
    }
}

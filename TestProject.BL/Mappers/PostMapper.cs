using TestProject.DAL.Models;
using TestProject.BL.Models;

namespace TestProject.BL.Mappers
{
    public static class PostMapper
    {
        public static Post MapPostEditorModelToPost(PostEditorModel postEditorModel)
        {
            return new Post
            {
                Title = postEditorModel.Title,
                Content = postEditorModel.Content
            };
        }
    }
}

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

        public static PostModel MapPostToPostModel(Post post)
        {
            return new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Author = post.User
            };
        }
    }
}

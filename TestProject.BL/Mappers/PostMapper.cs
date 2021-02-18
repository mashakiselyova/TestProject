using System;
using TestProject.DAL.Models;
using TestProject.BL.Models;

namespace TestProject.BL.Mappers
{
    public static class PostMapper
    {
        public static Post MapPostEditorModelToPost(PostEditorModel postEditorModel, int userId)
        {
            return new Post
            {
                Title = postEditorModel.Title,
                Content = postEditorModel.Content,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Rating = 0,
                UserId = userId
            };
        }
    }
}

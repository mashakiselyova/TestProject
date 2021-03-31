using TestProject.BL.Models;
using TestProject.DAL.Models;
using TestProject.Enums;

namespace TestProject.BL.Mappers
{
    public class RatingMapper : IMapper<RatingModel, PostRating>
    {
        public RatingModel ToBlModel(PostRating rating)
        {
            return new RatingModel
            {
                Value = (RatingButtonPosition)rating.Value,
                PostId = rating.PostId
            };
        }

        public PostRating ToDalModel(RatingModel rating)
        {
            return new PostRating
            {
                Value = (RatingValue)rating.Value,
                PostId = rating.PostId
            };
        }
    }
}

using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class RatingMapper : IMapper<RatingModel, Rating>
    {
        public RatingModel ToBlModel(Rating rating)
        {
            return new RatingModel
            {
                Value = rating.Value,
                PostId = rating.PostId
            };
        }

        public Rating ToDalModel(RatingModel rating)
        {
            return new Rating
            {
                Value = rating.Value,
                PostId = rating.PostId
            };
        }
    }
}

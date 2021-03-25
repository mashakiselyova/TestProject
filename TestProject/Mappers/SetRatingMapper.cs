using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class SetRatingMapper : IMapper<SetRatingModel, RatingModel>
    {
        public RatingModel ToBlModel(SetRatingModel model)
        {
            return new RatingModel
            {
                Value = model.Value,
                PostId = model.PostId
            };
        }

        public SetRatingModel ToWebModel(RatingModel model)
        {
            return new SetRatingModel
            {
                Value = model.Value,
                PostId = model.PostId
            };
        }
    }
}

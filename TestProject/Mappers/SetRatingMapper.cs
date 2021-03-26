using TestProject.BL.Enums;
using TestProject.BL.Models;
using TestProject.Enums;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class SetRatingMapper : IMapper<SetRatingModel, RatingModel>
    {
        public RatingModel ToBlModel(SetRatingModel model)
        {
            return new RatingModel
            {
                Value = (RatingButtonPosition)model.Value,
                PostId = model.PostId
            };
        }

        public SetRatingModel ToWebModel(RatingModel model)
        {
            return new SetRatingModel
            {
                Value = (ButtonPosition)model.Value,
                PostId = model.PostId
            };
        }
    }
}

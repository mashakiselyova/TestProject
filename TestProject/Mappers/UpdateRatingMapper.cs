using TestProject.BL.Enums;
using TestProject.Enums;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class UpdateRatingMapper : IMapper<UpdateRatingModel, BL.Models.UpdateRatingModel>
    {
        public BL.Models.UpdateRatingModel ToBlModel(UpdateRatingModel model)
        {
            return new BL.Models.UpdateRatingModel
            {
                TotalRating = model.TotalRating,
                RatingByCurrentUser = (RatingButtonPosition)model.RatingByCurrentUser
            };
        }

        public UpdateRatingModel ToWebModel(BL.Models.UpdateRatingModel model)
        {
            return new UpdateRatingModel
            {
                TotalRating = model.TotalRating,
                RatingByCurrentUser = (ButtonPosition)model.RatingByCurrentUser
            };
        }
    }
}

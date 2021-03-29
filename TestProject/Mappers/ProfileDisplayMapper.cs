using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class ProfileDisplayMapper : IMapper<ProfileDisplayModel, UserProfile>
    {
        public UserProfile ToBlModel(ProfileDisplayModel model)
        {
            return new UserProfile
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Rating = model.Rating
            };
        }

        public ProfileDisplayModel ToWebModel(UserProfile model)
        {
            return new ProfileDisplayModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Rating = model.Rating
            };
        }
    }
}

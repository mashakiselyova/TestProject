using System;
using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class ProfileDisplayMapper : IMapper<ProfileDisplayModel, UserProfile>
    {
        public UserProfile ToBlModel(ProfileDisplayModel model)
        {
            throw new NotImplementedException();
        }

        public ProfileDisplayModel ToWebModel(UserProfile model)
        {
            return new ProfileDisplayModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email
            };
        }
    }
}

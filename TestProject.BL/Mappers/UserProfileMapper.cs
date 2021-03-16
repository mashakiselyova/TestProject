using System;
using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class UserProfileMapper : IMapper<UserProfile, User>
    {
        public UserProfile ToBlModel(User model)
        {
            return new UserProfile
            {
                Id = model.Id,
                Name = $"{model.FirstName} {model.LastName}",
                Email = model.Email
            };
        }

        public User ToDalModel(UserProfile model)
        {
            throw new NotImplementedException();
        }
    }
}

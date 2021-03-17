using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class UserProfileMapper : IMapper<UserProfile, User>
    {
        public UserProfile ToBlModel(User user)
        {
            return new UserProfile
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };
        }

        public User ToDalModel(UserProfile profile)
        {
            var name = profile.Name.Split(' ');
            return new User
            {
                Id = profile.Id,
                FirstName = name[0],
                LastName = name[1],
                Email = profile.Email
            };
        }
    }
}

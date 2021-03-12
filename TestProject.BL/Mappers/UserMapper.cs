using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public static class UserMapper
    {
        public static User MapUserLoginModelToUser(UserLoginModel userLoginModel)
        {
            return new User
            {
                FirstName = userLoginModel.FirstName,
                LastName = userLoginModel.LastName,
                Email = userLoginModel.Email
            };
        }

        public static UserLoginModel MapUserToUserLoginModel(User user)
        {
            return new UserLoginModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public static UserProfile MapUserToUserProfile(User user)
        {
            return new UserProfile
            {
                Id = user.Id,
                Name = user.FirstName + ' ' + user.LastName,
                Email = user.Email
            };
        }
    }
}

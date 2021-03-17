using System;
using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class UserLoginMapper : IMapper<UserLoginModel, User>
    {
        public UserLoginModel ToBlModel(User user)
        {
            return new UserLoginModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public User ToDalModel(UserLoginModel user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}

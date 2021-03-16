using System;
using TestProject.BL.Models;
using TestProject.DAL.Models;

namespace TestProject.BL.Mappers
{
    public class UserLoginMapper : IMapper<UserLoginModel, User>
    {
        public UserLoginModel ToBlModel(User model)
        {
            throw new NotImplementedException();
        }

        public User ToDalModel(UserLoginModel model)
        {
            return new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }
    }
}

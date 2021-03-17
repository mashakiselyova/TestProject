using TestProject.BL.Models;
using TestProject.Models;

namespace TestProject.Mappers
{
    public class LoginMapper : IMapper<LoginModel, UserLoginModel>
    {
        public UserLoginModel ToBlModel(LoginModel model)
        {
            return new UserLoginModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }

        public LoginModel ToWebModel(UserLoginModel model)
        {
            return new LoginModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;

namespace TestProject.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorizationFilter]
        [Route("getUserProfile")]
        public UserProfile GetUserProfile()
        {
            var userEmail = (User.Identity as ClaimsIdentity).Claims
                .FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value;
            return _userService.GetUserProfile(userEmail);
        }
    }
}

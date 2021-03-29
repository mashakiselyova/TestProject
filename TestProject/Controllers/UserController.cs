using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TestProject.BL.Exceptions;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper<ProfileDisplayModel, UserProfile> _profileDisplayMapper;

        public UserController(IUserService userService, 
            IMapper<ProfileDisplayModel, UserProfile> profileDisplayMapper)
        {
            _userService = userService;
            _profileDisplayMapper = profileDisplayMapper;
        }

        [CustomAuthorizationFilter]
        [Route("getUserProfile")]
        public ActionResult<ProfileDisplayModel> GetUserProfile()
        {
            try
            {
                var userEmail = (User.Identity as ClaimsIdentity).Claims
                .FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value;
                var userProfile = _userService.GetProfile(userEmail);
                return _profileDisplayMapper.ToWebModel(userProfile);
            }
            catch (UserNotFoundException)
            {
                return StatusCode(404);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}

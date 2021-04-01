using Microsoft.AspNetCore.Mvc;
using System;
using TestProject.BL.Exceptions;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;
using TestProject.Utils;

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
        [Route("getCurrentUser")]
        public ActionResult<ProfileDisplayModel> GetCurrentUser()
        {
            try
            {
                var userProfile = _userService.GetByEmail(User.GetEmail());
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

        [Route("get/{id}")]
        public ActionResult<ProfileDisplayModel> Get([FromRoute] int id)
        {
            try
            {
                var userProfile = _userService.Get(id);
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

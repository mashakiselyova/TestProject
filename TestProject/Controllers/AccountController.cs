using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Services;
using TestProject.BL.Models;
using TestProject.Models;
using TestProject.Mappers;

namespace TestProject.Controllers
{
    [Authorize]    
    [Route("account")]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IMapper<LoginModel, UserLoginModel> _loginMapper;

        public AccountController(IUserService userService, IMapper<LoginModel, UserLoginModel> loginMapper)
        {
            _userService = userService;
            _loginMapper = loginMapper;
        }

        [AllowAnonymous]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims;
            var user = new LoginModel
            {
                FirstName = claims.FirstOrDefault(claim => claim.Type.Contains("givenname")).Value,
                LastName = claims.FirstOrDefault(claim => claim.Type.Contains("surname")).Value,
                Email = claims.FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value
            };
            await _userService.AddOrUpdate(_loginMapper.ToBlModel(user));

            return Redirect("/");
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}


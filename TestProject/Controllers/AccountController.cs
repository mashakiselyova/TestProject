﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Services;
using TestProject.BL.Models;

namespace TestProject.Controllers
{
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

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
            var user = new UserLoginModel
            {
                FirstName = claims.FirstOrDefault(claim => claim.Type.Contains("givenname")).Value,
                LastName = claims.FirstOrDefault(claim => claim.Type.Contains("surname")).Value,
                Email = claims.FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value
            };
            user = await _userService.AddUserAsync(user);

            return Json(user);
        }
    }
}

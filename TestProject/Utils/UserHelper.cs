using System.Linq;
using System.Security.Claims;

namespace TestProject.Utils
{
    public static class UserHelper
    {
        public static string GetUserEmail(ClaimsPrincipal user)
        {
            return (user.Identity as ClaimsIdentity).Claims
                    .FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value;
        }
    }
}

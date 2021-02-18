using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TestProject.BL.Models;
using TestProject.BL.Services;

namespace TestProject.Controllers
{
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("createPost")]
        public IActionResult Create([FromBody]PostEditorModel post)
        {
            _postService.Create(post, GetUserEmail());

            return new StatusCodeResult(201);
        }

        private string GetUserEmail()
        {
            return (User.Identity as ClaimsIdentity).Claims
                    .FirstOrDefault(claim => claim.Type.Contains("emailaddress")).Value;
        }
    }
}

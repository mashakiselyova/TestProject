using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Utils;

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
        [CustomAuthorizationFilter]
        public IActionResult Create([FromBody]PostEditorModel post)
        {
            _postService.Create(post, User.GetEmail());

            return new StatusCodeResult(201);
        }

        [Route("getAllPosts")]
        public async Task<List<PostDisplayModel>> GetAllPostsAsync()
        {
            var posts = await _postService.GetAllPostsAsync();
            return posts.Select(p => PostMapper.MapPostModelToPostModel(p)).ToList();
        }
    }
}

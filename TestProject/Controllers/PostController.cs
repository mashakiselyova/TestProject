using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;
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
        public async Task<IActionResult> CreateAsync([FromBody]CreatePostModel post)
        {
            await _postService.Create(post, User.GetEmail());

            return new StatusCodeResult(201);
        }

        [Route("getAllPosts")]
        public async Task<List<PostDisplayModel>> GetAllPostsAsync()
        {
            var posts = await _postService.GetAll();
            return posts.Select(PostMapper.MapPostModelToPostModel).ToList();
        }

        [Route("getUserPosts/{id}")]
        public async Task<List<PostDisplayModel>> GetUserPostsAsync([FromRoute] int id)
        {
            var posts = await _postService.GetUserPosts(id);
            return posts.Select(PostMapper.MapPostModelToPostModel).ToList();
        }

        [Route("/posts/getPost/{id}")]
        public async Task<EditPostModel> GetPostAsync([FromRoute] int id)
        {
            return await _postService.Get(id);
        }

        [HttpPost]
        [Route("/posts/editPost")]
        public async Task<IActionResult> EditPostAsync([FromBody] EditPostModel post)
        {
            await _postService.Edit(post);
            return Ok();
        }

        [HttpPost]
        [Route("/posts/delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _postService.Delete(id);
            return Ok();
        }
    }
}

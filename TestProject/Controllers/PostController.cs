using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;

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

        [Route("getPosts/{userId?}")]
        public async Task<List<PostDisplayModel>> GetPosts([FromRoute] int? userId)
        {
            var posts = await _postService.GetPosts(userId);
            return posts.Select(PostMapper.MapPostModelToPostModel).ToList();
        }

        [Route("/posts/get/{id}")]
        public async Task<EditPostModel> Get([FromRoute] int id)
        {
            return await _postService.Get(id);
        }

        [HttpPost]
        [CustomAuthorizationFilter]
        [Route("/posts/edit")]
        public async Task<IActionResult> Edit([FromBody] EditPostModel post)
        {
            if (post.Id == 0)
            {
                await _postService.Create(post);
            }
            else
            {
                await _postService.Edit(post);
            }            
            return Ok();
        }

        [HttpPost]
        [Route("/posts/delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _postService.Delete(id);
            return Ok();
        }
    }
}

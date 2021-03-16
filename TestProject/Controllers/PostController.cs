using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("getAll/{userId?}")]
        public async Task<List<PostDisplayModel>> GetAll([FromRoute] int? userId)
        {
            var posts = await _postService.GetPosts(userId);
            return posts.Select(PostMapper.MapPostModelToPostModel).ToList();
        }

        [Route("/posts/get/{id}")]
        public async Task<EditPostModel> Get([FromRoute] int id)
        {
            return PostMapper.MapEditPostModelDlToWeb(await _postService.Get(id));
        }

        [HttpPost]
        [CustomAuthorizationFilter]
        [Route("/posts/edit")]
        public async Task<IActionResult> Edit([FromBody] EditPostModel post)
        {
            if (post.Id == 0)
            {
                await _postService.Create(PostMapper.MapEditPostModelWebToBl(post), User.GetEmail());
            }
            else
            {
                await _postService.Edit(PostMapper.MapEditPostModelWebToBl(post), User.GetEmail());
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

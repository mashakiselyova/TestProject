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
        private IMapper<EditPostModel, BL.Models.EditPostModel> _editPostMapper;
        private IMapper<PostDisplayModel, BL.Models.PostModel> _displayPostMapper;

        public PostController(IPostService postService, 
            IMapper<EditPostModel, BL.Models.EditPostModel> editPostMapper,
            IMapper<PostDisplayModel, BL.Models.PostModel> displayPostMapper)
        {
            _postService = postService;
            _editPostMapper = editPostMapper;
            _displayPostMapper = displayPostMapper;
        }

        [Route("getAll/{userId?}")]
        public async Task<List<PostDisplayModel>> GetAll([FromRoute] int? userId)
        {
            var posts = await _postService.GetAll(userId, User.Identity.IsAuthenticated ? User.GetEmail() : null);
            return posts.Select(_displayPostMapper.ToWebModel).ToList();
        }

        [Route("/posts/get/{id}")]
        public async Task<EditPostModel> Get([FromRoute] int id)
        {
            return _editPostMapper.ToWebModel(await _postService.GetById(id));
        }

        [HttpPost]
        [CustomAuthorizationFilter]
        [Route("/posts/edit")]
        public async Task<IActionResult> Edit([FromBody] EditPostModel post)
        {
            try
            {
                if (post.Id == 0)
                {
                    await _postService.Create(_editPostMapper.ToBlModel(post), User.GetEmail());
                }
                else
                {
                    await _postService.Edit(_editPostMapper.ToBlModel(post), User.GetEmail());
                }
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
            
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

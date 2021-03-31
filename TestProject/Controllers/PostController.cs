using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;
using TestProject.Utils;
using TestProject.BL.Exceptions;
using System;

namespace TestProject.Controllers
{
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        private IMapper<EditPostModel, BL.Models.EditPostModel> _editPostMapper;
        private IMapper<PostDisplayModel, BL.Models.PostModel> _displayPostMapper;
        private IMapper<RichPostDisplayModel, BL.Models.RichPostModel> _richPostDisplayMapper;

        public PostController(IPostService postService, 
            IMapper<EditPostModel, BL.Models.EditPostModel> editPostMapper,
            IMapper<PostDisplayModel, BL.Models.PostModel> displayPostMapper,
            IMapper<RichPostDisplayModel, BL.Models.RichPostModel> richPostMapper)
        {
            _postService = postService;
            _editPostMapper = editPostMapper;
            _displayPostMapper = displayPostMapper;
            _richPostDisplayMapper = richPostMapper;
        }

        [Route("getAll/{userId?}")]
        public async Task<List<PostDisplayModel>> GetAll([FromRoute] int? userId)
        {
            var posts = await _postService.GetAll(userId, User.Identity.IsAuthenticated ? User.GetEmail() : null);
            return posts.Select(_displayPostMapper.ToWebModel).ToList();
        }

        [Route("/posts/get/{id}")]
        public async Task<ActionResult<EditPostModel>> Get([FromRoute] int id)
        {
            try
            {
                return _editPostMapper.ToWebModel(await _postService.GetById(id));
            }
            catch(PostNotFoundException)
            {
                return StatusCode(404);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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

        [Route("/posts/getRichPost/{id}")]
        public RichPostDisplayModel GetRichPost([FromRoute] int id)
        {
            var userEmail = User.Identity.IsAuthenticated ? User.GetEmail() : null;
            return _richPostDisplayMapper.ToWebModel(_postService.GetRichPost(id, userEmail));
        }
    }
}

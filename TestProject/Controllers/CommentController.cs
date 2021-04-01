using Microsoft.AspNetCore.Mvc;
using System;
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
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        private IMapper<CreateCommentModel, BL.Models.CreateCommentModel> _createCommentMapper;

        public CommentController(ICommentService commentService, 
            IMapper<CreateCommentModel, BL.Models.CreateCommentModel> createCommentMapper)
        {
            _commentService = commentService;
            _createCommentMapper = createCommentMapper;
        }

        [HttpPost]
        [Route("create")]
        [CustomAuthorizationFilter]
        public async Task<IActionResult> Create([FromBody] CreateCommentModel comment)
        {
            try
            {
                await _commentService.Create(_createCommentMapper.ToBlModel(comment), User.GetEmail());
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Services;
using TestProject.Filters;
using TestProject.Mappers;
using TestProject.Models;
using TestProject.Utils;

namespace TestProject.Controllers
{
    [Route("rating")]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;
        private IMapper<SetRatingModel, RatingModel> _setRatingMapper;
        private IMapper<Models.UpdateRatingModel, BL.Models.UpdateRatingModel> _updateRatingMapper;

        public RatingController(IRatingService ratingService, 
            IMapper<SetRatingModel, RatingModel> setRatingMapper,
            IMapper<Models.UpdateRatingModel, BL.Models.UpdateRatingModel> updateRatingMapper)
        {
            _ratingService = ratingService;
            _setRatingMapper = setRatingMapper;
            _updateRatingMapper = updateRatingMapper;
        }

        [Route("set")]
        [CustomAuthorizationFilter]
        public async Task<IActionResult> Set([FromBody] SetRatingModel setRatingModel)
        {
            try
            {
                await _ratingService.Set(_setRatingMapper.ToBlModel(setRatingModel), User.GetEmail());
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }            
        }

        [Route("get/{postId}")]
        [CustomAuthorizationFilter]
        public Models.UpdateRatingModel Get([FromRoute] int postId)
        {
            var result = _updateRatingMapper.ToWebModel(_ratingService.GetUpdatedRating(postId, User.GetEmail()));
            return result;
        }
    }
}

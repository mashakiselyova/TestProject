using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestProject.BL.Models;
using TestProject.BL.Services;
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

        public RatingController(IRatingService ratingService, IMapper<SetRatingModel, RatingModel> setRatingMapper)
        {
            _ratingService = ratingService;
            _setRatingMapper = setRatingMapper;
        }

        [Route("set")]
        public async Task<IActionResult> Set([FromBody] SetRatingModel setRatingModel)
        {
            await _ratingService.Set(_setRatingMapper.ToBlModel(setRatingModel), User.GetEmail());

            return Ok();
        }

        [Route("check/{postId}")]
        public bool CheckIfRated([FromRoute] int postId)
        {
            return _ratingService.CheckIfRated(postId, User.GetEmail());
        }

        [Route("get/{postId}")]
        public int Get([FromRoute] int postId)
        {
            return _ratingService.Get(postId);
        }
    }
}

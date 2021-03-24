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

        public RatingController(IRatingService ratingService, IMapper<SetRatingModel, RatingModel> setRatingMapper)
        {
            _ratingService = ratingService;
            _setRatingMapper = setRatingMapper;
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
    }
}

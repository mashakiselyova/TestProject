﻿using TestProject.BL.Models;
using TestProject.DAL.Models;
using TestProject.Enums;

namespace TestProject.BL.Mappers
{
    public class RatingMapper : IMapper<RatingModel, Rating>
    {
        public RatingModel ToBlModel(Rating rating)
        {
            return new RatingModel
            {
                Value = (RatingButtonPosition)rating.Value,
                PostId = rating.PostId
            };
        }

        public Rating ToDalModel(RatingModel rating)
        {
            return new Rating
            {
                Value = (RatingValue)rating.Value,
                PostId = rating.PostId
            };
        }
    }
}

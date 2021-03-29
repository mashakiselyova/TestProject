using System.Collections.Generic;
using System.Linq;
using TestProject.DAL.Models;
using TestProject.Enums;

namespace TestProject.BL.Utils
{
    public static class RatingHelper
    {
        public static int CalculateRating(List<Rating> ratings)
        {
            var pluses = ratings.Where(r => r.Value == RatingValue.Plus).ToArray().Length;
            var minuses = ratings.Where(r => r.Value == RatingValue.Minus).ToArray().Length;
            return pluses - minuses;
        }
    }
}

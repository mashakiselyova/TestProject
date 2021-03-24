using System;

namespace TestProject.BL.Exceptions
{
    public class RatingFailedException : Exception
    {
        public RatingFailedException()
            : base("Couldn't set rating")
        {
        }
    }
}

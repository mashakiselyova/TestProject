using System;

namespace TestProject.BL.Exceptions
{
    public class RatingFailedException : Exception
    {
        public RatingFailedException(string message)
            : base(message)
        {
        }
    }
}

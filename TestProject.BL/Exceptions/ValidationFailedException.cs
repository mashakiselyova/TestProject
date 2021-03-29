using System;

namespace TestProject.BL.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException(string message)
            : base(message)
        {
        }
    }
}

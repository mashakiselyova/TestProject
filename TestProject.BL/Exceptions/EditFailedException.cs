using System;

namespace TestProject.BL.Exceptions
{
    public class EditFailedException : Exception
    {
        public EditFailedException()
            : base("Couldn't edit post")
        {
        }
    }
}

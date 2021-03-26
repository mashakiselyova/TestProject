using System;

namespace TestProject.BL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            :base("User not found")
        {
        }
    }
}

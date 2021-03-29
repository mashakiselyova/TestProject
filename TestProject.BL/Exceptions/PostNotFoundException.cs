using System;

namespace TestProject.BL.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException()
            :base("Post not found")
        {
        }
    }
}

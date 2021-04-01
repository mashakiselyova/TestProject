using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface ICommentService
    {
        Task Create(CreateCommentModel comment, string userEmail);
    }
}

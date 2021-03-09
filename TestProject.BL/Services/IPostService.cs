﻿using System.Threading.Tasks;
using TestProject.BL.Models;

namespace TestProject.BL.Services
{
    public interface IPostService
    {
        Task Create(PostEditorModel postEditorModel, string userEmail);
    }
}
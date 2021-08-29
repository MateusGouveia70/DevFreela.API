using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        ProjectDetailsViewModel GetById(int id);
        List<ProjectViewModel> GetAll(string query);
        int CreateProject(CreateProjectInputModel inputmodel);
        void UpdateProject(UpdateProjectInputModel inputmodel);
        void CreateComment(CreateCommentInputModel inputmodel);
        void DeleteProject(int id); 
        void StartProject(int id);
        void FinishProject(int id);
    }
}

using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            return projects;

        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            var projectDetails = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartAt,
                project.FinishedAt);

            return projectDetails;

        }

        public int CreateProject(CreateProjectInputModel inputmodel)
        {
            Project project = new Project(
                inputmodel.Title,
                inputmodel.Description,
                inputmodel.IdFreelancer,
                inputmodel.IdClient,
                inputmodel.TotalCost);

            _dbContext.Projects.Add(project);

            return project.Id;
        }


        public void UpdateProject(UpdateProjectInputModel inputmodel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputmodel.Id);

            project.Update(inputmodel.Title, inputmodel.Description, inputmodel.TotalCost);

        }

        public void CreateComment(CreateCommentInputModel inputmodel)
        {
            ProjectComment comment = new ProjectComment(inputmodel.Content, inputmodel.IdProject, inputmodel.IdUser);

            _dbContext.Comments.Add(comment);
        }


        public void DeleteProject(int id)
        {
          var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Cancel();
        }


        public void FinishProject(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();
        }

        public void StartProject(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();
        }
    }
}

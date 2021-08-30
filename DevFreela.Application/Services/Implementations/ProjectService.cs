using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
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
            var project = _dbContext.Projects
                .Include(p => p.Freelancer)
                .Include(p => p.Client)
                .SingleOrDefault(p => p.Id == id);

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
                project.FinishedAt,
                project.Client.FullName,
                project.Freelancer.FullName);

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
            _dbContext.SaveChanges();

            return project.Id;
        }


        public void UpdateProject(int id, UpdateProjectInputModel inputmodel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Update(inputmodel.Title, inputmodel.Description, inputmodel.TotalCost);
            _dbContext.SaveChanges();

        }

        public void CreateComment(CreateCommentInputModel inputmodel)
        {
            ProjectComment comment = new ProjectComment(inputmodel.Content, inputmodel.IdProject, inputmodel.IdUser);

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }


        public void DeleteProject(int id)
        {
          var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Cancel();
            _dbContext.SaveChanges();
        }


        public void FinishProject(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();
            _dbContext.SaveChanges();
        }

        public void StartProject(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();
            //_dbContext.SaveChanges();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartAt = @startAt WHERE id = @id";

                sqlConnection.Execute(script, new { status = project.Status, startAt = project.StartAt, id });

            }
        }

    }
}

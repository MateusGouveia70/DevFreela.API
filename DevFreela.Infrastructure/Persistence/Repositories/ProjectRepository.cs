using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext; 
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);

            project.Start();

            await _dbContext.SaveChangesAsync();

            using (var SqlConnection = new SqlConnection(_connectionString))
            {
                SqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartAt = @startAt WHERE Id == @id";

                await SqlConnection.ExecuteAsync(script, new { status = project.Status, startAt = project.StartAt, project.Id });
            }
        }
    }
}

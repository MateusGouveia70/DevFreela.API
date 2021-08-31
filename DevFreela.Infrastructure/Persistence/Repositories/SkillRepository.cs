using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;

        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }
        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var SqlConnection = new SqlConnection(_connectionString))
            {
                SqlConnection.Open();

                var scrpit = "SELECT Id, Description FROM Skills";

                 var skills = await SqlConnection.QueryAsync<SkillDTO>(scrpit);

                return skills.ToList();
            }
        }
    }
}

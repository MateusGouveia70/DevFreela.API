using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly string _connectionString;

        public SkillsController(ISkillService skillService, IConfiguration configuration)
        {
            _skillService = skillService;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _skillService.GetAll();


            return Ok(skills);
        }
    }
}

using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(string query)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectModel projectModel)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, projectModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject([FromBody] ProjectModel project)
        {
            if (project.ProjectName.Length >= 200)
            {
                return BadRequest("Nome mto grande");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult CreateComment([FromBody] CreateCommentModel commentModel)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult StartProject(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult FinishProjetct(int id)
        {
            return NoContent();
        }
    }
}

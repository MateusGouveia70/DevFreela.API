using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
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
       private readonly IProjectService _projectService;

       public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetAll(string query)
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectInputModel inputModel)
        {
            var id = _projectService.CreateProject(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            _projectService.UpdateProject(id, inputModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            _projectService.DeleteProject(id);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult CreateComment([FromBody] CreateCommentInputModel inputModel) 
        {
            // validacao
            _projectService.CreateComment(inputModel);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult StartProject(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            _projectService.StartProject(id);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult FinishProjetct(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            _projectService.FinishProject(id);

            return NoContent();
        }
    }
}

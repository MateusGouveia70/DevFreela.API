using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FInishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Repositories;
using MediatR;
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
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getAllProjectsQuery = new GetAllProjectsQuery();

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var getProjectByIdQuery = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(getProjectByIdQuery);

            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectCommand command)
        {
            var updateProject = new UpdateProjectCommand(id);

            var project = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteProjectCommand = new DeleteProjectCommand(id);

            var delete = await _mediator.Send(deleteProjectCommand);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command) 
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartProject(int id)
        {
            var startProjectCommand = new StartProjectCommand(id);

            await _mediator.Send(startProjectCommand);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> FinishProjetct(int id)
        {
            var finishProjectCommand = new FinishProjectCommand(id);

            await _mediator.Send(finishProjectCommand);

            return NoContent();
        }
    }
}

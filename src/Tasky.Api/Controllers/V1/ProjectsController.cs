namespace Tasky.Api.Controllers.V1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Api.Request.Projects;
using Tasky.Application.Features.Projects.Commands.CreateProject;
using Tasky.Application.Features.Projects.Commands.DeleteProject;
using Tasky.Application.Features.Projects.Commands.UpdateProject;
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Application.Features.Projects.Queries.GetAllProjects;
using Tasky.Application.Features.Projects.Queries.GetProjectById;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects")]
public class ProjectsController(ISender _sender) : ApiController
{
  [HttpPost]
  [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [EndpointName("CreateProject")]
  [EndpointSummary("Creates a new project")]
  [EndpointDescription("Creates a new project with the specified name and description.")]
  public async Task<IActionResult> CreateProject(CreateProjectRequest request, CancellationToken ct = default)
  {

    var command = new CreateProjectCommand(request.Name, request.Description);

    var result = await _sender.Send(command, ct);


    return result.Match(
      id => CreatedAtAction(nameof(GetProjectById), new { Id = id }, id),
      errors => Problem(errors)
    );


  }

  [HttpGet("{Id:guid}")]
  [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [EndpointName("GetProjectById")]
  [EndpointSummary("Gets a project by Id")]
  [EndpointDescription("Gets a project by its unique identifier.")]
  public async Task<IActionResult> GetProjectById(Guid Id, CancellationToken ct = default)
  {
    var query = new GetProjectByIdQuery(Id);

    var result = await _sender.Send(query, ct);

    return result.Match(
      project => Ok(project),
      errors => Problem(errors)
    );

  }

  [HttpGet]
  [ProducesResponseType(typeof(List<ProjectDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [EndpointName("GetAllProjects")]
  [EndpointSummary("Gets all projects")]
  [EndpointDescription("Gets a list of all projects.")]
  public async Task<IActionResult> GetAllProjects(CancellationToken ct = default)
  {

    var result = await _sender.Send(new GetAllProjectsQuery(), ct);

    return result.Match(
      projects => Ok(projects),
      errors => Problem(errors)
    );
  }

  [HttpDelete("{Id:guid}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [EndpointName("DeleteProject")]
  [EndpointSummary("Deletes a project")]
  [EndpointDescription("Deletes a project by its unique identifier.")]
  public async Task<IActionResult> DeleteProject(Guid Id, CancellationToken ct = default)
  {
    var command = new DeleteProjectCommand(Id);

    var result = await _sender.Send(command, ct);

    return result.Match(
      _ => NoContent(),
      errors => Problem(errors)
    );
  }


  [HttpPut("{Id:guid}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [EndpointName("UpdateProject")]
  [EndpointSummary("Updates a project")]
  [EndpointDescription("Updates a project by its unique identifier.")]
  public async Task<IActionResult> UpdateProject(Guid Id, UpdateProjectRequest request, CancellationToken ct = default)
  {
    var command = new UpdateProjectCommand(Id, request.Name, request.Description);
    var result = await _sender.Send(command, ct);

    return result.Match(
      _ => NoContent(),
      errors => Problem(errors)
    );
  }
}
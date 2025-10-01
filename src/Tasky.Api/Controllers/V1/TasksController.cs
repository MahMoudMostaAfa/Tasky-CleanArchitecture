using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tasky.Api.Controllers.V1;
using Tasky.Api.Request.ProjectTasks;
using Tasky.Application.Features.Projects.Commands.DeleteProject;
using Tasky.Application.Features.Projects.Queries.GetProjectById;
using Tasky.Application.Features.ProjectTasks.Commands.CreateProjectTask;
using Tasky.Application.Features.ProjectTasks.Commands.DeleteProjectTask;
using Tasky.Application.Features.ProjectTasks.Commands.UpdateStatus;
using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using Tasky.Application.Features.ProjectTasks.Queries.GetProjectTasks;


[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects/{ProjectId:guid}/tasks")]
public class TasksController(ISender sender) : ApiController
{


  [HttpGet]
  [ProducesResponseType(typeof(List<ProjectTaskDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("GetTasksForProject")]
  [EndpointSummary("Retrieves all tasks for a specific project")]
  [EndpointDescription("Fetches and returns a list of all tasks associated with the specified project.")]
  public async Task<IActionResult> GetTasksForProject(Guid ProjectId, CancellationToken ct = default)
  {
    var query = new GetProjectTasksQuery(ProjectId);

    var result = await sender.Send(query, ct);

    return result.Match(
      tasks => Ok(tasks),
      errors => Problem(errors)
    );
  }


  [HttpGet("{TaskId:guid}")]
  [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("GetTaskById")]
  [EndpointSummary("Retrieves a specific task by its ID within a project")]
  [EndpointDescription("Fetches and returns the details of a specific task identified by its ID within the specified project.")]
  public async Task<IActionResult> GetTaskById(Guid ProjectId, Guid TaskId)
  {
    var query = new GetProjectTaskByIdQuery(ProjectId, TaskId);


    var result = await sender.Send(query);

    return result.Match(
      task => Ok(task),
      errors => Problem(errors)
    );
  }



  [HttpPost]
  [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("CreateTask")]
  [EndpointSummary("Creates a new task in a project")]
  [EndpointDescription("Creates a new task in the specified project with the given details.")]

  public async Task<IActionResult> CreateTask(Guid ProjectId, CreateProjectTaskRequest createProjectTaskRequest, CancellationToken ct = default)
  {

    var command = new CreateProjectTaskCommand(
      ProjectId: ProjectId,
      Title: createProjectTaskRequest.Title,
      Description: createProjectTaskRequest.Description,
      DueDateUtc: createProjectTaskRequest.DueDateUtc,
      Priority: createProjectTaskRequest.Priority,
      AssignedTo: createProjectTaskRequest.AssignedTo
    );

    var result = await sender.Send(command, ct);

    return result.Match(
      id => CreatedAtAction(nameof(GetTaskById), new { ProjectId = ProjectId, TaskId = id }, id),
      errors => Problem(errors)
    );
  }



  [HttpPut("{TaskId:guid}/change-status")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [EndpointName("UpdateTaskStatus")]
  [EndpointSummary("Updates the status of a specific task within a project")]
  [EndpointDescription("Updates the status of a specific task identified by its ID within the specified project.")]
  public async Task<IActionResult> UpdateTaskStatus(Guid ProjectId, Guid TaskId, UpdateTaskStatusRequest updateTaskStatusRequest, CancellationToken ct = default)
  {
    var command = new UpdateStatusCommand(
      ProjectId: ProjectId,
     TaskId: TaskId,
     Status: updateTaskStatusRequest.Status
    );
    var result = await sender.Send(command, ct);
    return result.Match(
      _ => NoContent(),
      errors => Problem(errors)
    );

  }


  [HttpDelete("{TaskId:guid}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [EndpointName("DeleteTaskById")]
  [EndpointSummary("Deletes a specific task by its ID within a project")]
  [EndpointDescription("Deletes a specific task identified by its ID within the specified project.")]
  public async Task<IActionResult> DeleteTaskById(Guid ProjectId, Guid TaskId)
  {
    var command = new DeleteProjectTaskCommand(ProjectId, TaskId);
    var result = await sender.Send(command);
    return result.Match(
      _ => NoContent(),
      errors => Problem(errors
    ));
  }

}
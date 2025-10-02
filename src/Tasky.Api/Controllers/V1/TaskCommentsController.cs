using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Features.TaskComments.Commands.CreateTaskComment;
using Tasky.Application.Features.TaskComments.Commands.DeleteTaskComment;
using Tasky.Application.Features.TaskComments.Commands.UpdateTaskComment;
using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Application.Features.TaskComments.Queries.GetCommentById;
using Tasky.Application.Features.TaskComments.Queries.GetCommentsByTask;

namespace Tasky.Api.Controllers.V1;


[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/tasks/{TaskId:guid}/comments")]
public class TaskCommentsController(ISender sender) : ApiController
{

  [HttpPost]
  [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("CreateTaskComment")]
  [EndpointSummary("Creates a new comment on a task")]
  [EndpointDescription("Creates a new comment on the specified task with the given details.")]
  public async Task<IActionResult> CreateComment(Guid TaskId, [FromBody] CreateCommentRequest request, CancellationToken ct)
  {

    var command = new CreateTaskCommentCommand(TaskId, request.Content);
    var result = await sender.Send(command, ct);

    return result.Match(
      commentId => CreatedAtAction(nameof(GetCommentById), new { TaskId, id = commentId }, commentId),
      errors => Problem(errors)
    );

  }


  [HttpPut("{CommentId:guid}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("UpdateTaskComment")]
  [EndpointSummary("Updates an existing comment on a task")]
  [EndpointDescription("Updates the specified comment on the task with the given details.")]

  public async Task<IActionResult> Update(Guid TaskId, Guid CommentId, [FromBody] UpdateCommentRequest request, CancellationToken ct)
  {
    var command = new UpdateTaskCommentCommand(TaskId, CommentId, request.Content);
    var result = await sender.Send(command, ct);

    return result.Match(
      _ => NoContent(),
      errors => Problem(errors)
    );
  }


  [HttpDelete("{CommentId:guid}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [EndpointName("DeleteTaskComment")]
  [EndpointSummary("Deletes a specific comment by its ID within a task")]
  [EndpointDescription("Deletes a specific comment identified by its ID within the specified task.")]
  public async Task<IActionResult> Delete(Guid TaskId, Guid CommentId, CancellationToken ct)
  {
    var command = new DeleteTaskCommentCommand(TaskId, CommentId);
    var result = await sender.Send(command, ct);

    return result.Match(
      _ => NoContent(),
      errors => Problem(errors)
    );
  }



  [HttpGet("{id:guid}")]
  [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)
  ]
  [EndpointName("GetCommentById")]
  [EndpointSummary("Retrieves a specific comment by its ID within a task")]
  [EndpointDescription("Retrieves a specific comment identified by its ID within the specified task.")]
  public async Task<IActionResult> GetCommentById(Guid TaskId, Guid id, CancellationToken ct)
  {
    var query = new GetCommentByIdQuery(TaskId, id);
    var result = await sender.Send(query, ct);

    return result.Match(
      comment => Ok(comment),
      errors => Problem(errors)
    );

  }



  [HttpGet]
  [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [EndpointName("GetCommentsByTask")]
  [EndpointSummary("Retrieves all comments for a specific task")]
  [EndpointDescription("Retrieves all comments associated with the specified task.")]
  public async Task<IActionResult> GetCommentsByTask(Guid TaskId, CancellationToken ct)
  {
    var query = new GetCommentsByTaskQuery(TaskId);
    var result = await sender.Send(query, ct);

    return result.Match(
      comments => Ok(comments),
      errors => Problem(errors)
    );

  }
}
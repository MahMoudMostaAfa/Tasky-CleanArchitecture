using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.TaskComments.Dtos;
using Tasky.Application.Features.TaskComments.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.TaskComments.Queries.GetCommentsByTask;

public class GetCommentsByTaskQueryHandler(ICurrentUser currentUser, ITaskRepository taskRepository) : IRequestHandler<GetCommentsByTaskQuery, Result<List<CommentDto>>>
{
  public async Task<Result<List<CommentDto>>> Handle(GetCommentsByTaskQuery request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var taskResult = await taskRepository.GetWithDetailsByIdAsync(request.TaskId);
    if (taskResult.IsError) return taskResult.Errors;
    var task = taskResult.Value;
    var TaskComments = task.Comments;
    if (task.AssignedTo != userId && task.Project.OwnerId != userId)
    {
      return ApplicationErrors.UserNotAuthorized;
    }

    return TaskComments.ToDtoList();


  }
}
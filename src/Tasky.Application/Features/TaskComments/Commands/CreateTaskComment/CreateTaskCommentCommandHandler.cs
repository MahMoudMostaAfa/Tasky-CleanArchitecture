using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;
using Tasky.Domain.ProjectTasks.Comments;

namespace Tasky.Application.Features.TaskComments.Commands.CreateTaskComment;

public class CreateTaskCommentCommandHandler(ICurrentUser currentUser, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateTaskCommentCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken)
  {

    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var taskResult = await taskRepository.GetWithDetailsByIdAsync(request.TaskId);

    if (taskResult.IsError) return taskResult.Errors;

    var AssignedTo = taskResult.Value.AssignedTo;
    var CreatedBy = taskResult.Value.Project?.OwnerId;

    if (AssignedTo != userId && CreatedBy != userId)
    {
      return ApplicationErrors.UserNotAuthorized;
    }

    var commentResult = TaskComment.Create(request.Content, request.TaskId, userId);
    if (commentResult.IsError) return commentResult.Errors;

    await taskRepository.AddCommentAsync(commentResult.Value);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return commentResult.Value.Id;
  }
}
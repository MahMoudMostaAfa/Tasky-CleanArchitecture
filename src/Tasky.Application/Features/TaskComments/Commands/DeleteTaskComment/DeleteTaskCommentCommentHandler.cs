using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.TaskComments.Commands.DeleteTaskComment;

public class DeleteTaskCommentHandler(ICurrentUser currentUser, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTaskCommentCommand, Result<Deleted>>
{
  public async Task<Result<Deleted>> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var taskResult = await taskRepository.GetByIdAsync(request.TaskId);
    if (taskResult.IsError) return taskResult.Errors;

    var commentResult = await taskRepository.GetCommentByIdAsync(request.CommentId);
    if (commentResult.IsError) return commentResult.Errors;

    var comment = commentResult.Value;
    if (comment.AuthorId != userId) return ApplicationErrors.UserNotAuthorized;

    taskRepository.DeleteComment(comment);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Deleted;
  }
}
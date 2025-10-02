using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.TaskComments.Commands.UpdateTaskComment;

public class UpdateTaskCommentCommandHandler(ICurrentUser currentUser, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateTaskCommentCommand, Result<Updated>>
{
  public async Task<Result<Updated>> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;
    var TaskResult = await taskRepository.GetByIdAsync(request.TaskId);
    if (TaskResult.IsError) return TaskResult.Errors;

    var commentResult = await taskRepository.GetCommentByIdAsync(request.CommentId);
    if (commentResult.IsError) return commentResult.Errors;

    var comment = commentResult.Value;
    if (comment.AuthorId != userId) return ApplicationErrors.UserNotAuthorized;


    var updateResult = comment.UpdateContent(request.Content);
    if (updateResult.IsError) return updateResult.Errors;


    await unitOfWork.SaveChangesAsync(cancellationToken);


    return Result.Updated;
  }
}
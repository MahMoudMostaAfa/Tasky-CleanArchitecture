using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.ProjectTasks.Commands.UpdateStatus;

public class UpdateStatusCommandHandler(ICurrentUser currentUser, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateStatusCommand, Result<Updated>>
{
  public async Task<Result<Updated>> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
  {

    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var TaskResult = await taskRepository.GetWithDetailsByIdAsync(request.TaskId);

    if (TaskResult.IsError) return TaskResult.Errors;

    var task = TaskResult.Value;

    if (task.Project?.OwnerId != userId)
    {
      return ApplicationErrors.UserNotAuthorized;
    }
    var updateResult = task.UpdateStatus(request.Status);

    if (updateResult.IsError) return updateResult.Errors;
    await unitOfWork.SaveChangesAsync(cancellationToken);
    return Result.Updated;
  }
}
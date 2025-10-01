using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.ProjectTasks.Commands.DeleteProjectTask;
using Tasky.Domain.Common.Results;
using Tasky.Domain.ProjectTasks;

public class DeleteProjectTaskCommandHandler(ICurrentUser currentUser, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectTaskCommand, Result<Deleted>>
{
  public async Task<Result<Deleted>> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUser.UserId;
    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;

    var projectTaskResult = await taskRepository.GetWithDetailsByIdAsync(request.TaskId);
    if (projectTaskResult.IsError) return projectTaskResult.Errors;
    var projectTask = projectTaskResult.Value;
    if (projectTask.ProjectId != request.ProjectId) return ProjectTaskErrors.ProjectTaskNotFound;
    if (projectTask.Project?.OwnerId != userId) return ApplicationErrors.UserNotAuthorized;

    taskRepository.Delete(projectTask);

    await unitOfWork.SaveChangesAsync();

    return Result.Deleted;
  }
}
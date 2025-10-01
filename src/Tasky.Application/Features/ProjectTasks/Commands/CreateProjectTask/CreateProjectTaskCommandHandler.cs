using MediatR;
using Tasky.Application.Common.Errors;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.ProjectTasks.Commands.CreateProjectTask;


public class CreateProjectTaskCommandHandler(ICurrentUser currentUser, IProjectRepository projectRepository, ITaskRepository taskRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProjectTaskCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
  {

    var userId = currentUser.UserId;

    if (userId is null) return ApplicationErrors.UserIdClaimInvalid;


    var project = await projectRepository.GetByIdAsync(request.ProjectId);

    if (project.IsError) return project.Errors;

    if (project.Value.OwnerId != userId) return ApplicationErrors.UserNotAuthorized;


    var projectTaskResult = ProjectTask.Create(
      title: request.Title,
      description: request.Description,
      projectId: request.ProjectId,
      dueDate: request.DueDateUtc,
      priority: request.Priority,
      assignedTo: request.AssignedTo
    );

    if (projectTaskResult.IsError) return projectTaskResult.Errors;


    await taskRepository.AddAsync(projectTaskResult.Value);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return projectTaskResult.Value.Id;
  }
}
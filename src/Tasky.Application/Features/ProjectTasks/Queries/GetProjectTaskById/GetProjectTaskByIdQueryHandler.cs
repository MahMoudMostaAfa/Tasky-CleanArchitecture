using MediatR;
using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Application.Features.ProjectTasks.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTaskById;


public class GetProjectTaskByIdQueryHandler(IProjectRepository projectRepository, ITaskRepository taskRepository) : IRequestHandler<GetProjectTaskByIdQuery, Result<ProjectTaskDto>>
{
  public async Task<Result<ProjectTaskDto>> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
  {
    var ProjectResult = await projectRepository.GetByIdAsync(request.ProjectId);
    if (ProjectResult.IsError) return ProjectResult.Errors;

    var TaskResult = await taskRepository.GetByIdAsync(request.TaskId);
    if (TaskResult.IsError) return TaskResult.Errors;

    if (TaskResult.Value.ProjectId != request.ProjectId)
      return ProjectTaskErrors.ProjectTaskNotFound;


    return TaskResult.Value.ToDto();
  }

}

using MediatR;
using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Application.Features.ProjectTasks.Mappers;
using Tasky.Domain.Common.Results;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.ProjectTasks.Queries.GetProjectTasks;

public class GetProjectTasksQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectTasksQuery, Result<List<ProjectTaskDto>>>
{
  public async Task<Result<List<ProjectTaskDto>>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
  {
    var projectResult = await projectRepository.GetWithTasksByIdAsync(request.ProjectId);

    if (projectResult.IsError) return projectResult.Errors;

    var projectTasks = projectResult.Value.ProjectTasks;


    return projectTasks.ToDtoList();

  }
}
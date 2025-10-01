using Tasky.Application.Features.ProjectTasks.Dtos;
using Tasky.Domain.ProjectTasks;

namespace Tasky.Application.Features.ProjectTasks.Mappers;

public static class ProjectTaskMapper
{

  public static ProjectTaskDto ToDto(this ProjectTask projectTask)
  {
    return new ProjectTaskDto
    {
      Id = projectTask.Id,
      Title = projectTask.Title,
      Description = projectTask.Description,
      CreatedAt = projectTask.CreatedAt,
      DueDateUtc = projectTask.DueDateUtc,
      Priority = projectTask.Priority,
      Status = projectTask.Status,
      AssignedTo = projectTask.AssignedTo
    };
  }

  public static List<ProjectTaskDto> ToDtoList(this IEnumerable<ProjectTask> projectTasks)
  {
    return projectTasks.Select(pt => pt.ToDto()).ToList();
  }
}
using Tasky.Application.Features.Projects.Dtos;
using Tasky.Domain.Projects;

namespace Tasky.Application.Features.Projects.Mappers;


public static class ProjectMapper
{


  public static ProjectDto ToDto(this Project project)
  {

    ArgumentNullException.ThrowIfNull(project);

    return new ProjectDto
    {
      Id = project.Id,
      Name = project.Name,
      Description = project.Description,
      CreatedAt = project.CreatedAt,
      ModifiedAt = project.ModifiedAt
    };
  }


  public static List<ProjectDto> ToDtoList(this IEnumerable<Project> projects)
  {
    ArgumentNullException.ThrowIfNull(projects);

    return projects.Select(P => P.ToDto()).ToList();
  }
}
using Tasky.Domain.ProjectTasks.Enums;

namespace Tasky.Application.Features.ProjectTasks.Dtos;

public class ProjectTaskDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? DueDateUtc { get; set; }
  public ProjectTaskPriority Priority { get; set; }
  public ProjectTaskStatus Status { get; set; }
  public string? AssignedTo { get; set; }

}



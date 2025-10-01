using Tasky.Domain.ProjectTasks.Enums;

namespace Tasky.Api.Request.ProjectTasks;

public class CreateProjectTaskRequest
{
  public string Title { get; set; } = null!;

  public string? Description { get; set; }

  public DateTime? DueDateUtc { get; set; }

  public ProjectTaskPriority? Priority { get; set; }

  public string? AssignedTo { get; set; }
}
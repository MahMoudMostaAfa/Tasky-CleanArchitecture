using Tasky.Domain.ProjectTasks.Enums;

namespace Tasky.Api.Request.ProjectTasks;

public class UpdateTaskStatusRequest
{
  public ProjectTaskStatus Status { get; set; }

}
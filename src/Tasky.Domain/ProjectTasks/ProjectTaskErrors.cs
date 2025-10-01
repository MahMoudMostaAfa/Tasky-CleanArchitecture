using Tasky.Domain.Common.Results;

namespace Tasky.Domain.ProjectTasks;

public static class ProjectTaskErrors
{
  public static Error TitleIsRequired => Error.Validation(
  code: "ProjectTask.TitleIsRequired",
  description: "Title is required"
);

  public static Error ProjectIdIsRequired => Error.Validation(
    code: "ProjectTask.ProjectIdIsRequired",
    description: "ProjectId is required"
  );
  public static Error ProjectTaskNotFound => Error.NotFound(
    code: "ProjectTask.NotFound",
    description: "Project task not found"
  );

  public static Result<Updated> StatusUnchanged => Error.Validation(
    code: "ProjectTask.StatusUnchanged",
    description: "The new status is the same as the current status"
  );
}

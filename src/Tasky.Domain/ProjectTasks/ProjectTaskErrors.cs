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

}

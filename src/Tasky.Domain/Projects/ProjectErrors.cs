using Tasky.Domain.Common.Results;

namespace Tasky.Domain.Projects;

public class ProjectErrors
{
  public static Error InValidOwnerId => Error.Validation(
    code: "Project.InvalidOwnerId",
    description: "OwnerId is required"
  );
  public static Error NameIsRequired => Error.Validation(
    code: "Project.NameIsRequired",
    description: "Name is required"
  );


}
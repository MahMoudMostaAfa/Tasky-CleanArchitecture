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

  public static Error ProjectNotFound => Error.NotFound(
    code: "Project.NotFound",
    description: "Project not found"
  );

  public static Error UserNotAuthorized => Error.Unauthorized(
    code: "Project.UserNotAuthorized",
    description: "User is not authorized to access this project"
  );


}
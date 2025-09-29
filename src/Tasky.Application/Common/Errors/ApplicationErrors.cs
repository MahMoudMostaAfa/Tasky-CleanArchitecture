using Tasky.Domain.Common.Results;

namespace Tasky.Application.Common.Errors;

public class ApplicationErrors
{

    public static Error UserNotFound => Error.NotFound(
        code: "Auth.User.NotFound",
        description: "User not found.");
    public static Error UserIdClaimInvalid => Error.Conflict(
        code: "Auth.UserIdClaim.Invalid",
        description: "Invalid userId claim.");

    public static Error UserNotAuthorized => Error.Unauthorized(
        code: "Auth.User.NotAuthorized",
        description: "User is not authorized to perform this action.");
}
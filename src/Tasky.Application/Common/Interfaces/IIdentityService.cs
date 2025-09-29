using Tasky.Application.Features.Identity;
using Tasky.Application.Features.Identity.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Common.Interfaces;

public interface IIdentityService
{
  Task<bool> IsInRoleAsync(string userId, string role);

  Task<Result<UserDto>> RegisterUserAsync(RegisterUserRequest request, string userRole = "User");

  Task<bool> AssignUserToRoleAsync(string userId, string role);

  Task<Result<UserDto>> GetUserByIdAsync(string userId);

  Task<Result<UserDto>> AuthenticateAsync(string email, string password);


}
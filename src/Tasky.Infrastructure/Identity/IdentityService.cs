using Microsoft.AspNetCore.Identity;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Identity;
using Tasky.Application.Features.Identity.Dtos;

using Tasky.Domain.Common.Results;

namespace Tasky.Infrastructure.Identity;

public class IdentityService(UserManager<AppUser> _userManager) : IIdentityService
{
  public async Task<bool> AssignUserToRoleAsync(string userId, string role)
  {
    var user = await _userManager.FindByIdAsync(userId);

    if (user == null)
    {
      return false;
    }

    var Result = await _userManager.AddToRoleAsync(user, role);

    return Result.Succeeded;
  }

  public async Task<Result<UserDto>> AuthenticateAsync(string email, string password)
  {
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null) return Error.NotFound(code: "User.NotFound", description: "User not found");

    var result = await _userManager.CheckPasswordAsync(user, password);
    if (!result) return Error.Unauthorized(code: "User.Unauthorized", description: "Invalid credentials");

    return new UserDto
    {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email!,
      Roles = await _userManager.GetRolesAsync(user)
    };
  }

  public async Task<Result<UserDto>> GetUserByIdAsync(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null) return Error.NotFound(code: "User.NotFound", description: "User not found");
    return new UserDto
    {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email!,
      Roles = await _userManager.GetRolesAsync(user)
    };
  }

  public async Task<bool> IsInRoleAsync(string userId, string role)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null)
    {
      return false;
    }
    return await _userManager.IsInRoleAsync(user, role);

  }

  public async Task<Result<UserDto>> RegisterUserAsync(RegisterUserRequest request, string role = "User")
  {
    var user = new AppUser
    {
      FirstName = request.FirstName,
      LastName = request.LastName,
      Email = request.Email,
      UserName = request.Email
    };
    var result = await _userManager.CreateAsync(user, request.Password);

    if (!result.Succeeded)
    {
      return Error.Validation(code: "User.RegistrationFailed", description: "User registration failed");
    }

    await _userManager.AddToRoleAsync(user, role);

    return new UserDto
    {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email!,
      Roles = await _userManager.GetRolesAsync(user)
    };


  }
}

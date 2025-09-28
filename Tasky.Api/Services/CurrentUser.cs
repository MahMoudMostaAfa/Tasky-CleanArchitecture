using System.Security.Claims;
using Tasky.Application.Common.Interfaces;

namespace Tasky.Api.Services;

public class CurrentUser(IHttpContextAccessor _httpContextAccessor) : ICurrentUser
{
  public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Common.Interfaces;

public interface ITokenProvider
{
  string GenerateToken(UserDto user);
}
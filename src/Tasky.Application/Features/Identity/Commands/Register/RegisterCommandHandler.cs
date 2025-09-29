using MediatR;
using Microsoft.Extensions.Logging;
using Tasky.Application.Common.Interfaces;
using Tasky.Application.Features.Identity.Dtos;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Commands.Register;


public class RegisterCommandHandler(IIdentityService identityService, ITokenProvider tokenProvider, ILogger<RegisterCommandHandler> logger) : IRequestHandler<RegisterCommand, Result<string>>
{
  public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var result = await identityService.RegisterUserAsync(new RegisterUserRequest
    {
      FirstName = request.FirstName,
      LastName = request.LastName,
      Email = request.Email,
      Password = request.Password
    }, "User");

    if (!result.IsSuccess)
    {
      logger.LogWarning("User registration failed for email: {Email}. Reason: {Reason}", request.Email, result.Errors);
      return result.Errors;
    }

    logger.LogInformation("User {Email} registered successfully.", request.Email);
    var token = tokenProvider.GenerateToken(result.Value);

    return token;

  }
}
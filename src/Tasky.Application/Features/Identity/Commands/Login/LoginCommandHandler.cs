using MediatR;
using Microsoft.Extensions.Logging;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results;

namespace Tasky.Application.Features.Identity.Commands.Login;



public class LoginCommandHandler(IIdentityService identityService, ILogger<LoginCommandHandler> logger, ITokenProvider tokenProvider) : IRequestHandler<LoginCommand, Result<string>>
{

  public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var userResult = await identityService.AuthenticateAsync(request.Email, request.Password);
    if (!userResult.IsSuccess)
    {
      logger.LogWarning("Authentication failed for email: {Email}. Reason: {Reason}", request.Email, userResult.Errors);
      return userResult.Errors;
    }

    var token = tokenProvider.GenerateToken(userResult.Value);

    logger.LogInformation("User {Email} authenticated successfully.", request.Email);
    return token;



  }


}
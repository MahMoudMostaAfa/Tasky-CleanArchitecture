

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;
using Tasky.Application.Common.Interfaces;

namespace Tasky.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, ICurrentUser user, IIdentityService identityService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;
    private readonly ICurrentUser _user = user;
    private readonly IIdentityService _identityService = identityService;

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.UserId ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            var result = await _identityService.GetUserByIdAsync(userId);

            if (result.IsSuccess) userName = result.Value.Email;
        }

        _logger.LogInformation(
            "Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);
    }
}
using System.Diagnostics;


using MediatR;

using Microsoft.Extensions.Logging;
using Tasky.Application.Common.Interfaces;

namespace Tasky.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUser _user;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUser user,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.UserId ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _identityService.GetUserByIdAsync(userId);

                if (result.IsSuccess) userName = result.Value.Email;
            }

            _logger.LogWarning(
                "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}", requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
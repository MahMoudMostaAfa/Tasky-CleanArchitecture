using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Tasky.Domain.Projects.Events;

namespace Tasky.Application.Features.Projects.EventHandlers;

public class ModifiedProjectEventHandler(HybridCache _cache, ILogger<ModifiedProjectEventHandler> logger) : INotificationHandler<ModifiedProjectEvent>
{
  public async Task Handle(ModifiedProjectEvent notification, CancellationToken cancellationToken)
  {
    await _cache.RemoveByTagAsync(notification.Tag);

    logger.LogInformation("invaliding  cache by tag name : {tage}", notification.Tag);
  }
}
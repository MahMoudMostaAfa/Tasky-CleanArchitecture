using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Tasky.Application.Common.Interfaces;
using Tasky.Domain.Common.Results.Abstractions;
namespace Tasky.Application.Common.Behaviours;


public class CachingBehaviour<TRequest, TResponse>(
HybridCache cache,
ILogger<CachingBehaviour<TRequest, TResponse>> logger

) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{

  private readonly HybridCache _cache = cache;
  private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger = logger;

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
  {

    if (request is not ICachedQuery cachedRequest)
    {
      return await next(ct);
    }
    _logger.LogInformation("Checking cache for {RequestName}", typeof(TRequest).Name);


    var result = await _cache.GetOrCreateAsync<TResponse>(
        cachedRequest.CacheKey,
        _ => new ValueTask<TResponse>((TResponse)(object)null!),
        new HybridCacheEntryOptions
        {
          // only get data from L1 do not go to l2 ,l3
          Flags = HybridCacheEntryFlags.DisableUnderlyingData
        },
        cancellationToken: ct);

    if (result is null)
    {
      result = await next(ct);

      if (result is IResult res && res.IsSuccess)
      {
        _logger.LogInformation("Caching result for {RequestName}", typeof(TRequest).Name);

        await _cache.SetAsync(
            cachedRequest.CacheKey,
            result,
            new HybridCacheEntryOptions
            {
              Expiration = cachedRequest.Expiration
            },
            cachedRequest.Tags,
            ct);
      }
    }


    return result;
  }
}
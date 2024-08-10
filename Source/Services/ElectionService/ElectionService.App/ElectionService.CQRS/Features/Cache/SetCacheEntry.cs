
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ElectionService.CQRS.Features.Cache;

/// <summary>
/// Represents the command used to set a query cache entry (means to cache a query result).
/// </summary>
public class SetQueryCacheEntry : IRequest<Unit>
{
	public string CacheKey { get; set; }
	public object CacheValue { get; set; }

	public SetQueryCacheEntry(string cacheKey, object cacheValue)
	{
		CacheKey = cacheKey;
		CacheValue = cacheValue;
	}
}



public class SetQueryCacheEntryHandler : IRequestHandler<SetQueryCacheEntry, Unit>
{
	readonly IDistributedCache _distributedCache;

	public SetQueryCacheEntryHandler(IDistributedCache distributedCache)
	{
		_distributedCache = distributedCache;
	}

	public async Task<Unit> Handle(SetQueryCacheEntry request, CancellationToken cancellationToken)
	{

		var cacheOptions = new DistributedCacheEntryOptions()
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
			SlidingExpiration = TimeSpan.FromMinutes(10)
		};

		var serializedValue = JsonSerializer.Serialize(request.CacheValue);

		await _distributedCache.SetStringAsync(request.CacheKey, serializedValue, cacheOptions, cancellationToken);

		return Unit.Value;
	}
}

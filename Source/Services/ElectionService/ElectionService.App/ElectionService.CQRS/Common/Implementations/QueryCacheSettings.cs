namespace ElectionService.CQRS.Common.Implementations;

/// <summary>
/// Represents the cache settings for a query.
/// </summary>
public class QueryCacheSettings : ICacheSettings
{
	public string? CacheKey { get; }
	public bool UseCacheIfAvailable { get; }



	public QueryCacheSettings()
	{
		UseCacheIfAvailable = false;
	}

	public QueryCacheSettings(string cacheKey, bool useCacheIfAvailable)
	{
		CacheKey = cacheKey;
		UseCacheIfAvailable = useCacheIfAvailable;
	}

	public QueryCacheSettings(string cacheKey) : this(cacheKey, useCacheIfAvailable: true)
	{
	}


	/// <summary>
	/// Creates a <see cref="QueryCacheSettings"/> with no cache settings
	/// </summary>
	public static QueryCacheSettings CreateNotCachedQuerySettings() => new();

	/// <summary>
	/// Creates a <see cref="QueryCacheSettings"/> with cache settings
	/// </summary>
	/// <param name="cacheKey">The cache key</param>
	public static QueryCacheSettings CreateCachedQuerySettings(string cacheKey) => new(cacheKey);
}
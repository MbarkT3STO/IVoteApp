namespace AuthService.Common.Interfaces;

/// <summary>
/// Represents the cache settings.
/// </summary>
public interface ICacheSettings
{
	/// <summary>
	/// Gets the cache key of the query.
	/// </summary>
	public string CacheKey { get; }

	/// <summary>
	/// Gets a value indicating whether the query should use the cache if it is available.
	/// </summary>
	public bool UseCacheIfAvailable { get; }
}
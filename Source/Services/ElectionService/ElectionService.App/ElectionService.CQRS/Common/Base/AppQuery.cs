
namespace ElectionService.CQRS.Common.Base;

/// <summary>
/// Represents a base class for query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
public abstract class AppQuery<TResult> : IQuery<TResult> where TResult : class
{
	public Guid QueryId { get; }

	public TResult? Result { get; }

	public string CacheKey { get; }

	public bool UseCacheIfAvailable { get; } = true;



	protected AppQuery()
	{
		QueryId = Guid.NewGuid();
	}

	protected AppQuery(string cacheKey)
	{
		QueryId = Guid.NewGuid();
		CacheKey = cacheKey;
	}

	protected AppQuery(Guid queryId, string cacheKey)
	{
		QueryId = queryId;
		CacheKey = cacheKey;
	}
}


/// <summary>
/// Represents a base class for query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
/// <typeparam name="TResultValue">The type of the query result value.</typeparam>
public abstract class AppQuery<TResult, TResultValue> : IQuery<TResult, TResultValue> where TResult : QueryResult<TResultValue, TResult>
{
	public Guid QueryId { get; } = Guid.NewGuid();

	public TResult Result { get; }

	public string CacheKey { get; }
	public bool UseCacheIfAvailable { get; } = true;
}

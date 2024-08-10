
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

	public int PageNumber { get; }

	public int PageSize { get; }

	public int PageIndex => PageNumber - 1;

	public int PageOffset => PageIndex * PageSize;


	// protected AppQuery()
	// {
	// 	QueryId = Guid.NewGuid();
	// }

	protected AppQuery(string cacheKey)
	{
		QueryId = Guid.NewGuid();
		CacheKey = cacheKey;
		UseCacheIfAvailable = true;
		PageNumber = 1;
		PageSize = 10;
	}

	protected AppQuery(string cacheKey, int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();
		CacheKey = cacheKey;
		UseCacheIfAvailable = true;
		PageNumber = pageNumber;
		PageSize = pageSize;
	}

	protected AppQuery(int pageNumber, int pageSize = 10)
	{
		QueryId = Guid.NewGuid();
		UseCacheIfAvailable = false;
		PageNumber = pageNumber;
		PageSize = pageSize;
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
	public int PageNumber { get; }
	public int PageSize { get; } = 10;
}

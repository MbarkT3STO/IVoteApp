namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents a query operation for the application.
/// </summary>
public interface IQuery
{
	/// <summary>
	/// Gets the identifier of the query.
	/// </summary>
	public Guid QueryId { get; }

	/// <summary>
	/// Gets the cache key of the query.
	/// </summary>
	public string CacheKey { get; }

	/// <summary>
	/// Gets a value indicating whether the query should use the cache if it is available.
	/// </summary>
	public bool UseCacheIfAvailable { get; }

	/// <summary>
	/// Gets the page number.
	/// </summary>
	public int PageNumber { get; }

	/// <summary>
	/// Gets the page size (number of items per page).
	/// </summary>
	public int PageSize { get; }

	/// <summary>
	/// Gets the index of the page (starting from 0).
	/// </summary>
	public int PageIndex => PageNumber - 1;

	/// <summary>
	/// Gets the offset of the page (means the number of items to skip).
	/// </summary>
	public int PageOffset => PageIndex * PageSize;
}


/// <summary>
/// Represents a query operation for the application.
/// </summary>
public interface IQuery<TResult> : IQuery, IRequest<TResult> where TResult : class
{
	/// <summary>
	/// Gets the result of the query.
	/// </summary>
	public TResult? Result { get; }
}


/// <summary>
/// Represents a query operation for the application.
/// This interface is also inherited from <see cref="IRequest{TResult}"/>
/// </summary>
public interface IQuery<TResult, TResultValue> : IQuery, IRequest<TResult> where TResult : QueryResult<TResultValue, TResult>
{
	/// <summary>
	/// Gets the result of the query.
	/// </summary>
	public TResult? Result { get; }
}

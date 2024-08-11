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
	/// Gets the cache settings of the query.
	/// </summary>
	public ICacheSettings CacheSettings { get; }

	/// <summary>
	/// Gets the pagination settings of the query.
	/// </summary>
	public IPaginationSettings PaginationSettings { get; }
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

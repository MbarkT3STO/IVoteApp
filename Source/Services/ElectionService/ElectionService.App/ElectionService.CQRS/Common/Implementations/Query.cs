
namespace ElectionService.CQRS.Common.Implementations;

/// <summary>
/// Represents a query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
public class Query<TResult> : IQuery<TResult> where TResult : class
{
	public Guid QueryId { get; set; }

	public TResult? Result { get; set; }
	public string CacheKey { get; }
}

/// <summary>
/// Represents a query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
public class Query<TResult, TResultValue> : IQuery<TResult, TResultValue> where TResult : QueryResult<TResultValue, TResult>
{
	public Guid QueryId { get; set; }

	public TResult? Result { get; set; }
	public string CacheKey { get; }

}

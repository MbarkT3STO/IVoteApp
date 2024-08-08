
namespace ElectionService.CQRS.Common.Base;

/// <summary>
/// Represents a base class for query operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the query result.</typeparam>
public abstract class AppQuery<TResult> : IQuery<TResult> where TResult : class
{
	public Guid QueryId { get; } = Guid.NewGuid();

	public TResult Result { get; }
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
}


namespace AuthService.Common.Implementations;


/// <summary>
/// Represents the result of a query operation that can either succeed with a value or fail with an error.
/// </summary>
/// <typeparam name="TValue">The type of the value returned upon success.</typeparam>
public abstract class AppQueryResult<TValue, TQueryResult> : IQueryResult<TValue> where TQueryResult : AppQueryResult<TValue, TQueryResult>
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error? Error { get; }
	public TValue? Value { get; }

	TValue? IQueryResult<TValue>.Value => Value;

	protected AppQueryResult(TValue? value)
	{
		IsSuccess = true;
		Value = value;
	}

	protected AppQueryResult(Error error)
	{
		IsSuccess = false;
		Error = error;
	}

	/// <summary>
	/// Creates a succeeded result with the specified value.
	/// </summary>
	/// <typeparam name="TQueryResult">The type of the query result.</typeparam>
	public static TQueryResult Succeeded(TValue value) => (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), value)!;


	/// <summary>
	/// Creates a failed result with the specified error.
	/// </summary>
	/// <typeparam name="TQueryResult">The type of the query result.</typeparam>
	public static TQueryResult Failed(Error error) => (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), error)!;

	/// <summary>
	/// Creates a failed result with the specified exception/error.
	/// </summary>
	/// <typeparam name="TQueryResult">The type of the query result.</typeparam>
	public static TQueryResult Failed(Exception exception) => (TQueryResult)Activator.CreateInstance(typeof(TQueryResult), new Error(exception.Message))!;
}
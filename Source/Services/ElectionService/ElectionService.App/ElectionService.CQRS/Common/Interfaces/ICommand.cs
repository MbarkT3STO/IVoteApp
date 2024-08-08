namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public interface ICommand<TResult> : IRequest<TResult> where TResult : class
{
	/// <summary>
	/// Gets the identifier of the command.
	/// </summary>
	public Guid CommandId { get; }

	/// <summary>
	/// Gets the result of the command.
	/// </summary>
	public TResult Result { get; }
}


/// <summary>
/// Represents a command operation for the application.
/// </summary>
public interface ICommand : IRequest
{
	/// <summary>
	/// Gets the identifier of the command.
	/// </summary>
	public Guid CommandId { get; }
}


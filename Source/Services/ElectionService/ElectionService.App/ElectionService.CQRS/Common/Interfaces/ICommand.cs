namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents a command operation for the application.
/// </summary>
public interface ICommand
{
	/// <summary>
	/// Gets the identifier of the command.
	/// </summary>
	public Guid CommandId { get; }

	/// <summary>
	/// Gets the timestamp of the command.
	/// </summary>
	public DateTime CommandTimestamp { get; }

	/// <summary>
	/// Gets the type of the command.
	/// </summary>
	public CommandType CommandType { get; }
}


/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public interface ICommand<TResult> : ICommand, IRequest<TResult> where TResult : class
{
	/// <summary>
	/// Gets the result of the command.
	/// </summary>
	public TResult? Result { get; }
}


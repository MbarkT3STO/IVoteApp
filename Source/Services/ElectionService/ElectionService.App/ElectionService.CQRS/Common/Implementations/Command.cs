namespace ElectionService.CQRS.Common.Implementations;

/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public class Command<TResult> : ICommand<TResult> where TResult : class
{
	public Guid CommandId { get; set; }

	public TResult Result { get; set; }
}

/// <summary>
/// Represents a command operation for the application.
/// </summary>
public class Command : ICommand
{
	public Guid CommandId { get; set; }
}

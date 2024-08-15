namespace AuthService.Common.Base;

/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public abstract class AppCommand<TResult>: ICommand<TResult> where TResult: class
{
	public Guid CommandId { get; } = Guid.NewGuid();
	public DateTime CommandTimestamp { get; }
	public CommandType CommandType { get; }

	public TResult? Result { get; set; }


	public AppCommand()
	{
		CommandId        = Guid.NewGuid();
		CommandTimestamp = DateTime.UtcNow;
	}


	/// <summary>
	/// Creates a new instance of the Command.
	/// </summary>
	public static AppCommand<TResult> Create() => (AppCommand<TResult>)Activator.CreateInstance(typeof(AppCommand<TResult>))!;
}

/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <typeparam name="TCommandResult">The type of the command result.</typeparam>
public abstract class AppCommand<TCommand, TCommandResult>: ICommand<TCommandResult> where TCommandResult: class where TCommand: AppCommand<TCommand, TCommandResult>
{
	public Guid CommandId { get; } = Guid.NewGuid();
	public DateTime CommandTimestamp { get; }
	public CommandType CommandType { get; }

	public TCommandResult? Result { get; set; }


	public AppCommand()
	{
		CommandId        = Guid.NewGuid();
		CommandTimestamp = DateTime.UtcNow;
	}


	/// <summary>
	/// Creates a new instance of the Command.
	/// </summary>
	public static AppCommand<TCommand, TCommandResult> Create() => (AppCommand<TCommand, TCommandResult>)Activator.CreateInstance(typeof(AppCommand<TCommand, TCommandResult>))!;
}

/// <summary>
/// Represents a command operation for the application.
/// </summary>
public class AppCommand: ICommand
{
	public Guid CommandId { get; } = Guid.NewGuid();

	public DateTime CommandTimestamp => throw new NotImplementedException();

	public CommandType CommandType => throw new NotImplementedException();
}


namespace ElectionService.CQRS.Common.Base;

/// <summary>
/// Represents a command operation for the application.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public abstract class AppCommand<TResult> : ICommand<TResult> where TResult : class
{
    public Guid CommandId { get; } = Guid.NewGuid();

    public TResult Result { get; }
}

/// <summary>
/// Represents a command operation for the application.
/// </summary>
public class AppCommand : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}


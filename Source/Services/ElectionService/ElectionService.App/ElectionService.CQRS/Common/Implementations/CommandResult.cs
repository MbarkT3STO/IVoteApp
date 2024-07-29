namespace ElectionService.CQRS.Common.Implementations;
/// <summary>
/// Represents the base result of a command execution.
/// </summary>
public abstract class CommandResult : ICommandResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }


    protected CommandResult(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
    protected CommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Error = isSuccess ? null : new Error("Unknown error");
    }
}


/// <summary>
/// Represents the base result of a command execution with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class CommandResult<TValue, TCommandResult> : ICommandResult<TValue> where TCommandResult : CommandResult<TValue, TCommandResult>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }
    public TValue Value { get; }


    protected CommandResult(TValue value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected CommandResult(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    protected CommandResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Error = isSuccess ? null : new Error("Unknown error");
    }


    /// <summary>
    /// Creates a succeeded <typeparamref name="TCommandResult"/> with the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A succeeded command result with the specified value.</returns>
    public static TCommandResult Succeeded(TValue value) => Activator.CreateInstance(typeof(TCommandResult), value) as TCommandResult;

    /// <summary>
    /// Creates a failed <typeparamref name="TCommandResult"/> with the specified error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>A failed command result with the specified error.</returns>
    public static TCommandResult Failed(Error error) => Activator.CreateInstance(typeof(TCommandResult), error) as TCommandResult;

    /// <summary>
    /// Creates a failed <typeparamref name="TCommandResult"/> with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>A failed command result with the specified error message.</returns>
    public static TCommandResult Failed(string errorMessage) => Failed(new Error(errorMessage));


    /// <summary>
    /// Creates a failed <typeparamref name="TCommandResult"/> with the specified exception.
    /// </summary>
    /// <typeparam name="TCommandResult">The type of the command result.</typeparam>
    /// <param name="exception">The exception.</param>
    public static TCommandResult Failed(Exception exception) => Failed(new Error(exception));
}
namespace AuthService.Common.Exceptions;

/// <summary>
/// Represents an error that occurred during the execution of an operation.
/// </summary>
public class Error
{
    public string Message { get; }
    public AppException? Exception { get; }

    public static readonly Error None = new(string.Empty);
    public static readonly Error NullValue = new("Value cannot be null.");


    public Error(string message)
    {
        Message = message;
        Exception = null;
    }

    public Error(Exception exception)
    {
        Message = exception.Message;
        Exception = new AppException(exception.Message, exception);
    }

    public Error(string message, AppException exception)
    {
        Message = message;
        Exception = exception;
    }

    public static Error FromException(AppException exception)
    {
        return new Error(exception.Message, exception);
    }

    public static Error FromException(Exception exception)
    {
        return new Error(exception.Message, new AppException(exception.Message, exception));
    }
}
namespace AuthService.Common.Exceptions;

/// <summary>
/// Represents an Exception that occurred during the execution of an operation.
/// </summary>
public class AppException : Exception
{
    public AppException(string message) : base(message)
    {
    }

    public AppException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public AppException(Error error) : base(error.Message)
    {
    }

    public AppException(IEnumerable<Error> errors) : base(string.Join("\n", errors.Select(error => error.Message)))
    {
    }
}
namespace AuthService.Common.Interfaces;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    bool IsFailure { get; }

    /// <summary>
    /// Gets the error associated with the operation, if any.
    /// </summary>
    Error Error { get; }
}



/// <summary>
/// Represents the result of an operation with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public interface IResult<out TValue> : IResult
{
    /// <summary>
    /// Gets the value associated with the operation.
    /// </summary>
    TValue Value { get; }
}
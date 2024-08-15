namespace AuthService.Common.Interfaces;


/// <summary>
/// Represents the result of a query operation that returns an <b>Object</b> value.
/// </summary>
public interface IQueryResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    Error? Error { get; }
    object? Value { get; }
}

/// <summary>
/// Represents the result of a query operation that returns a value of type <b><typeparamref name="TValue"/></b>.
/// </summary>
public interface IQueryResult<TValue>
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    Error? Error { get; }
    TValue? Value { get; }
}
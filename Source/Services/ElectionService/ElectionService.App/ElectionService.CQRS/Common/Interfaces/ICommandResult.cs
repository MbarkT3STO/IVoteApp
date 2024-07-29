namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents the result of a command operation.
/// </summary>
public interface ICommandResult : IResult
{

}

/// <summary>
/// Represents the result of a command operation.
/// </summary>
/// <typeparam name="T">The type of the value returned by the command.</typeparam>
public interface ICommandResult<out T> : IResult<T>
{

}
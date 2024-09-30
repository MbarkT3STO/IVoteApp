namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents an application event.
/// </summary>
/// <typeparam name="T">The type of data associated with the event.</typeparam>
public interface IAppEvent<T>: INotification
{
	/// <summary>
	/// Gets the details of the event.
	/// </summary>
	public IEventDetails EventDetails { get; }

	/// <summary>
	/// Gets the data associated with the event.
	/// </summary>
	public T EventData { get; }
}

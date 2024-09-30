namespace ElectionService.CQRS.Common.Implementations;

/// <summary>
/// Represents an application event.
/// </summary>
public class AppEvent<T>: IAppEvent<T>
{
	public IEventDetails EventDetails { get; }
	public T EventData { get; }


	protected AppEvent(IEventDetails eventDetails, T eventData)
	{
		EventDetails = eventDetails;
		EventData    = eventData;
	}


	/// <summary>
	/// Creates a new <see cref="AppEvent{T}"/> instance.
	/// </summary>
	/// <param name="eventDetails">The event details.</param>
	/// <param name="eventData">The event data.</param>
	/// <returns></returns>
	public static AppEvent<T> Create(IEventDetails eventDetails, T eventData)
	{
		return new AppEvent<T>(eventDetails, eventData);
	}

}

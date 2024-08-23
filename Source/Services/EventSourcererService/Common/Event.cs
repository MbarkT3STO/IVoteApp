namespace EventSourcererService.Common;

/// <summary>
/// Represents an event.
/// </summary>
public interface IEvent
{
	/// <summary>
	/// Gets or sets the unique identifier of the event.
	/// </summary>
	Guid Id { get; set; }

	/// <summary>
	/// Gets or sets the type of the event.
	/// </summary>
	string Type { get; set; }

	/// <summary>
	/// Gets or sets the timestamp of the event.
	/// </summary>
	DateTime TimeStamp { get; set; }

	/// <summary>
	/// Gets or sets the user ID associated with the event.
	/// </summary>
	string UserId { get; set; }
}


/// <summary>
/// Represents an event in the system.
/// </summary>
/// <typeparam name="T">The type of data associated with the event.</typeparam>
public abstract class Event<T>: IEvent where T: class
{
	public Guid Id { get; set; }
	public string Type { get; set; }
	public DateTime TimeStamp { get; set; }
	public string UserId { get; set; }
	public T JsonData { get; set; }

	protected Event()
	{
	}

	protected Event(Guid eventId, string type, DateTime timeStamp, string userId, T jsonData)
	{
		Id        = eventId;
		Type      = type;
		TimeStamp = timeStamp;
		UserId    = userId;
		JsonData  = jsonData;
	}
}

namespace ElectionService.CQRS.Common.Implementations;


/// <summary>
/// Represents the details of an event.
/// </summary>
public class EventDetails: IEventDetails
{
	public Guid EventId { get; } = Guid.NewGuid();
	public string EventName { get; }
	public string OccurredBy { get; }
	public DateTime OccurredOn { get; }

	public EventDetails(string eventName, string occurredBy, DateTime occurredOn)
	{
		EventName  = eventName;
		OccurredBy = occurredBy;
		OccurredOn = occurredOn;
	}

	public EventDetails(Guid eventId, string eventName, string occurredBy, DateTime occurredOn)
	{
		EventId    = eventId;
		EventName  = eventName;
		OccurredBy = occurredBy;
		OccurredOn = occurredOn;
	}
}

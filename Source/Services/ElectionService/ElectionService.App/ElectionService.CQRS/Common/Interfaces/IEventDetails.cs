namespace ElectionService.CQRS.Common.Interfaces;

/// <summary>
/// Represents the details of an event.
/// </summary>
public interface IEventDetails
{
	public Guid EventId { get; }
	public string EventName { get; }
	public string OccurredBy { get; }
	public DateTime OccurredOn { get; }
}

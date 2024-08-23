namespace RabbitMq.Messages.Abstractions;

/// <summary>
/// Represents a base event message.
/// </summary>
public abstract class BaseEventMessage : IEventMessage
{
	/// <summary>
	/// Gets the unique identifier of the event.
	/// </summary>
	public required Guid EventId { get; set; }

	/// <summary>
	/// Gets or sets the name of the event.
	/// </summary>
	public required string EventName { get; set; }

	/// <summary>
	/// Gets or sets the user identifier (ID) of the user that triggered the event.
	/// </summary>
	public required string UserId { get; set; }
}

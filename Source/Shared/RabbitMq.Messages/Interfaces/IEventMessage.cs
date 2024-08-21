namespace RabbitMq.Messages.Interfaces;

/// <summary>
/// Represents an event message.
/// </summary>
public interface IEventMessage
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    Guid EventId { get; }
}
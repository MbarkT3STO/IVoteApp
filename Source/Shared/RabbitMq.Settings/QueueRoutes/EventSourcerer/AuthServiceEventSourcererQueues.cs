namespace RabbitMq.Settings.QueueRoutes.EventSourcerer;

/// <summary>
/// Contains the queue names for events related to the AuthService.
/// </summary>
public static class AuthServiceEventSourcererQueues
{
	public const string UserCreatedEventQueue = "EventSourcererService-AuthService-UserCreatedEventsQueue";

	public static ISendEndpoint GetEndpoint(IBus bus, RabbitMqSettings rabbitMqSettings, string queueName)
	{
		return bus.GetEndpoint(rabbitMqSettings, queueName);
	}
}
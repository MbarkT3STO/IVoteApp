namespace RabbitMq.Settings.Extensions;

/// <summary>
/// Provides extension methods for getting send endpoints for queues.
/// </summary>
public static class QueueEndpointProviderExtensions
{
	/// <summary>
	/// Gets the send endpoint for the specified queue.
	/// </summary>
	/// <param name="bus">The bus.</param>
	/// <param name="rabbitMqSettings">The rabbit mq settings.</param>
	/// <param name="queueName">The queue name.</param>
	public static ISendEndpoint GetEndpoint(this IBus bus, RabbitMqSettings rabbitMqSettings, string queueName)
	{
		return bus.GetSendEndpoint(new Uri($"{rabbitMqSettings.HostName}/{queueName}")).GetAwaiter().GetResult();
	}
}
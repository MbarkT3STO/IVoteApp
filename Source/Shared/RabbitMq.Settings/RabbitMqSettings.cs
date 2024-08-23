namespace RabbitMq.Settings;

/// <summary>
/// Represents the RabbitMQ settings.
/// </summary>
public class RabbitMqSettings
{
	public string HostName { get; set; }
	public int Port { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectionService.CQRS.DI;

public static class RabbitMQRegistrar
{
	/// <summary>
	/// Configures RabbitMQ for the application using the provided configuration.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the RabbitMQ configuration to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> containing the RabbitMQ configuration settings.</param>
	public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
	{
		var rabbitMqSettings = configuration.GetSection("RabbitMQ:Settings").Get<RabbitMqSettings>();

		services.AddMassTransit(busConfigurator =>
		{
			busConfigurator.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(rabbitMqSettings.HostName, hostConfig =>
				{
					hostConfig.Username(rabbitMqSettings.UserName);
					hostConfig.Password(rabbitMqSettings.Password);
				});

				cfg.ReceiveEndpoint(ElectionServiceQueues.User.UserCreatedQueue, ep => ep.Consumer<UserCreatedMessageConsumer>(ctx));
			});
		});

		services.ConfigureRabbitMQBaseOptions(configuration);
	}
}

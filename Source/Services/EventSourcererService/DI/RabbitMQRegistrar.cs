using EventSourcererService.MessageConsumers.AuthService;
using MassTransit;
using RabbitMq.Settings.QueueRoutes.EventSourcerer;

namespace EventSourcererService.DI;

public static class RabbitMQRegistrar
{
	/// <summary>
	/// Configures RabbitMQ for the application using the provided configuration.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the RabbitMQ configuration to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> containing the RabbitMQ configuration settings.</param>
	public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
	{
		var rabbitMqOptions = configuration.GetSection("RabbitMQ:Settings").Get<RabbitMqSettings>();
		// var authServiceRabbitMqEndPointsOptions = configuration.GetSection("RabbitMQ:EndPoints:AuthService").Get<AuthServiceRabbitMqEndpointsOptions>();
		// var authServiceRabbitMqEndPoints = authServiceRabbitMqEndPointsOptions;

		services.AddMassTransit(busConfigurator =>
		{
			busConfigurator.UsingRabbitMq((context, cfg) =>
			{
				cfg.Host(rabbitMqOptions.HostName, hostConfig =>
				{
					hostConfig.Username(rabbitMqOptions.UserName);
					hostConfig.Password(rabbitMqOptions.Password);
				});

				cfg.ReceiveEndpoint(AuthServiceEventSourcererQueues.UserCreatedEventQueue, ep => ep.Consumer<AuthServiceUserCreatedMessageConsumer>(context));

			});
		});

		services.ConfigureRabbitMQBaseOptions(configuration);
	}
}
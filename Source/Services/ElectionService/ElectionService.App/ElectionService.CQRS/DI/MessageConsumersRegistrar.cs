using Microsoft.Extensions.DependencyInjection;

namespace ElectionService.CQRS.DI;

public static class MessageConsumersRegistrar
{
    /// <summary>
    /// Registers message consumers for RabbitMQ.
    /// </summary>
    /// <param name="services">The service collection to register the message consumers with.</param>
    public static void RegisterMessageConsumers(this IServiceCollection services)
    {
        services.AddScoped<UserCreatedMessageConsumer>();
    }
}


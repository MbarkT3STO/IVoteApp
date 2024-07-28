using Microsoft.Extensions.DependencyInjection;
namespace ElectionService.CQRS.DI;

public static class ApplicationDIExtensions
{
	/// <summary>
	/// Adds application services to the service collection
	/// </summary>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		// Add AutoMapper
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		// Add MediatR
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

		return services;
	}
}

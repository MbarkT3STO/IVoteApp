using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;

namespace ElectionService.CQRS.DI;

public static class SqlServerCacheDIExtensions
{
	/// <summary>
	/// Adds the sql server cache to the service collection
	/// </summary>
	public static IServiceCollection AddSqlServerCache(this IServiceCollection services, IConfigurationManager configuration)
	{
		services.AddDistributedSqlServerCache(options =>
		{
			options.ConnectionString = configuration.GetConnectionString("CacheConnection");
			options.SchemaName = "dbo";
			options.TableName = "Cache";
		});

		return services;
	}
}

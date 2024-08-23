namespace EventSourcererService.DATA;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}

	public DbSet<AuthServiceUserEvent> AuthService_UserEvents { get; set; }
	public DbSet<ElectionServicePoliticalPartyEvent> ElectionService_PoliticalPartyEvents { get; set; }
}

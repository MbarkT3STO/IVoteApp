using ElectionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectionService.Database;

public class AppDbContext : DbContext
{
	public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options )
	{

	}


	public virtual DbSet<PoliticalParty> PoliticalParties { get; set; }
	public virtual DbSet<Election> Elections { get; set; }
	public virtual DbSet<Candidate> Candidates { get; set; }

	public virtual DbSet<User> Users { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Apply entities configurations from the assembly
		modelBuilder.ApplyConfigurationsFromAssembly( typeof( AppDbContext ).Assembly );

		base.OnModelCreating(modelBuilder);
	}
}

namespace AuthService.DATA;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Apply configurations from assembly
		builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

		// Seed App Roles
		builder.Entity<AppRole>().HasData(
			new AppRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
			new AppRole { Id = "User", Name = "User", NormalizedName = "USER" }
		);

		// Seed App Users

		var passwordHasher = new PasswordHasher<AppUser>();
		var appUser = new AppUser
		{
			Id = "MBVRK",
			UserName = "mbvrk",
			NormalizedUserName = "MBVRK",
			Email = "mbvrk@example.com",
			FirstName = "MBVRK",
			LastName = "MBVRK",
			CreatedAt = DateTime.Now,
		};

		appUser.PasswordHash = passwordHasher.HashPassword(appUser, "123456");

		builder.Entity<AppUser>().HasData(appUser);

			// Seed App User Roles for mbvrk
		builder.Entity<AppUserRole>().HasData(
			new AppUserRole
			{
				UserId = "MBVRK",
				RoleId = "Admin"
			});

		base.OnModelCreating(builder);
	}


	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<AppRole> AppRoles { get; set; }
	public DbSet<AppUserRole> AppUserRoles { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }
}
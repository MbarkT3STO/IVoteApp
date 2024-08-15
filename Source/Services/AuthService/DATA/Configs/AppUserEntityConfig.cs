namespace AuthService.DATA.Configs;

public class AppUserEntityConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
    }
}
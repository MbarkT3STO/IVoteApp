namespace AuthService.DATA.Configs;

public class AppUserRoleEntityConfig : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        // builder.HasOne(x => x.AppUser)
        //     .WithMany(x => x.AppUserRoles)
        //     .HasForeignKey(x => x.UserId);

        // builder.HasOne(x => x.AppRole)
        //     .WithMany(x => x.AppUserRoles)
        //     .HasForeignKey(x => x.RoleId);
    }
}

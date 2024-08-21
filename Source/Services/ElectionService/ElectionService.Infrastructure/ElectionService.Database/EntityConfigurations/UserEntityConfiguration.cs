using ElectionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectionService.Database.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(user => user.Id);
	}
}

using ElectionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectionService.Database.EntityConfigurations;

/// <summary>
/// Configures the <see cref="Election"/> entity
/// </summary>
public class ElectionEntityConfiguration : IEntityTypeConfiguration<Election>
{
	/// <summary>
	/// Configures the entity
	/// </summary>
	/// <param name="builder">The builder</param>
	public void Configure( EntityTypeBuilder<Election> builder )
	{
		builder.ToTable( "Elections" );
		builder.HasKey( election => election.Id );
		builder.Property( election => election.Id ).ValueGeneratedNever();

		builder.Property( election => election.Title ).IsRequired();
		builder.Property( election => election.Description ).IsRequired();
		builder.Property( election => election.StartDateAndTime ).IsRequired();
		builder.Property( election => election.EndDateAndTime ).IsRequired();
		builder.Property( election => election.Status ).IsRequired();
		builder.Property( election => election.CreatedBy ).IsRequired();
	}
}

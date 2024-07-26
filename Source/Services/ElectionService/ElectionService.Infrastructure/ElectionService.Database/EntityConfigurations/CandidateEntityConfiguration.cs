using ElectionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectionService.Database.EntityConfigurations;

/// <summary>
/// Configures the <see cref="Candidate"/> entity
/// </summary>
public class CandidateEntityConfiguration : IEntityTypeConfiguration<Candidate>
{
	/// <summary>
	/// Configures the entity
	/// </summary>
	/// <param name="builder">The builder</param>
	public void Configure( EntityTypeBuilder<Candidate> builder )
	{
		builder.ToTable( "Candidates" );

		builder.HasKey( candidate => candidate.Id );
		builder.Property( candidate => candidate.Id ).ValueGeneratedNever();

		builder.Property( candidate => candidate.Name ).IsRequired();
		builder.Property( candidate => candidate.Description ).IsRequired();

		builder.Property( candidate => candidate.PhotoUrl ).IsRequired();

		builder.HasOne( candidate => candidate.Election ).WithMany( election => election.Candidates ).HasForeignKey( candidate => candidate.ElectionId );
	}
}

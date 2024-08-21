using ElectionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectionService.Database.EntityConfigurations;

public class PoliticalPartyEntityConfiguration : IEntityTypeConfiguration<PoliticalParty>
{
	public void Configure(EntityTypeBuilder<PoliticalParty> builder)
	{
		builder.ToTable("PoliticalParties");
		builder.HasKey(politicalParty => politicalParty.Id);
		builder.Property(politicalParty => politicalParty.Id).ValueGeneratedNever();

		builder.Property(politicalParty => politicalParty.Name).IsRequired();
		builder.Property(politicalParty => politicalParty.Description).IsRequired();
		builder.Property(politicalParty => politicalParty.EstablishmentDate).IsRequired();
		builder.Property(politicalParty => politicalParty.WebsiteUrl).IsRequired();
		builder.Property(politicalParty => politicalParty.LogoUrl).IsRequired();
		builder.Property(politicalParty => politicalParty.CreatedBy).IsRequired();

		builder.HasOne(politicalParty => politicalParty.CreatedByUser).WithMany(user => user.PoliticalParties).HasForeignKey(politicalParty => politicalParty.CreatedBy);
	}
}

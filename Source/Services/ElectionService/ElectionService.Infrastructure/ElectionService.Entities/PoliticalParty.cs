using ElectionService.Entities.Common.Implementations;

namespace ElectionService.Entities;

public class PoliticalParty : AuditableEntity<Guid>
{
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required DateTime EstablishmentDate { get; set; }
	public required string LogoUrl { get; set; }
	public required string WebsiteUrl { get; set; }

	public virtual List<Candidate> Candidates { get; set; }
	public virtual User CreatedByUser { get; set; }
}

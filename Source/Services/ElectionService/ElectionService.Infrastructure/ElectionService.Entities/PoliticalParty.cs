namespace ElectionService.Entities;

public class PoliticalParty
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required DateTime EstablishmentDate { get; set; }
	public required string LogoUrl { get; set; }
	public required string WebsiteUrl { get; set; }
	public required string CreatedBy { get; set; }

	public virtual List<Candidate> Candidates { get; set; }
}

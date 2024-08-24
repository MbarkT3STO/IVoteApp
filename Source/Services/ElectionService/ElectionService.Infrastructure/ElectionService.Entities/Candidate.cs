using ElectionService.Entities.Common.Implementations;

namespace ElectionService.Entities;

/// <summary>
/// Represent a candidate in an election
/// </summary>
public class Candidate : AuditableEntity<Guid>
{
	public required Guid ElectionId { get; set; }
	public required Guid PoliticalPartyId { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string PhotoUrl { get; set; }

	public virtual PoliticalParty PoliticalParty { get; set; }
	public virtual Election Election { get; set; }
	public virtual User CreatedByUser { get; set; }
}

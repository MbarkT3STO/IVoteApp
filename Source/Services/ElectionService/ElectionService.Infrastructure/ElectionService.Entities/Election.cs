using ElectionService.Entities.Common.Implementations;
using ElectionService.Shared.Enums;

namespace ElectionService.Entities;

/// <summary>
/// Represent an election
/// </summary>
public class Election : AuditableEntity<Guid>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime StartDateAndTime { get; set; }
	public DateTime EndDateAndTime { get; set; }
	public ElectionStatus Status { get; set; }

	public List<Candidate> Candidates { get; set; }
	public virtual User CreatedByUser { get; set; }
}

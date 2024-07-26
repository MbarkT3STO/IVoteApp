using ElectionService.Shared.Enums;

namespace ElectionService.Entities;

/// <summary>
/// Represent an election
/// </summary>
public class Election
{
	public Guid Id { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public DateTime StartDateAndTime { get; set; }

	public DateTime EndDateAndTime { get; set; }

	public ElectionStatus Status { get; set; }
	public string CreatedBy { get; set; }

	public List<Candidate> Candidates { get; set; }
}

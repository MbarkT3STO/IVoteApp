namespace ElectionService.Entities;

/// <summary>
/// Represent a candidate in an election
/// </summary>
public class Candidate
{
	public Guid Id { get; set; }
	public Guid ElectionId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string PhotoUrl { get; set; }

	public virtual Election Election { get; set; }
}

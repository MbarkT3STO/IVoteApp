namespace ElectionService.Entities;

public class User
{
	public string Id { get; set; }
	public string UserName { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string RoleId { get; set; }
	public string RoleName { get; set; }
	public bool IsActive { get; set; } = true;

	public virtual ICollection<PoliticalParty> PoliticalParties { get; set; }
	public virtual ICollection<Candidate> Candidates { get; set; }
	public virtual ICollection<Election> Elections { get; set; }
}

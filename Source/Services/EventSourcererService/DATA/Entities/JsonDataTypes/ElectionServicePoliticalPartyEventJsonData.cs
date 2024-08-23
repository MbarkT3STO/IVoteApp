namespace EventSourcererService.DATA.Entities.JsonDataTypes;

/// <summary>
/// Represents the JSON data for a political party event in the Election Service.
/// </summary>
public class ElectionServicePoliticalPartyEventJsonData: AuditableJsonEntity<Guid>
{
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }
	public string CreatedBy { get; set; }

	public ElectionServicePoliticalPartyEventJsonData(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, string createdBy, DateTime createdAt) : base(id, createdAt)
	{

	}
}

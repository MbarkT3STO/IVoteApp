namespace EventSourcererService.DATA.Entities.JsonDataTypes.ElectionService;

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

	public ElectionServicePoliticalPartyEventJsonData(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy): base(id, createdAt, createdBy)
	{
		Name              = name;
		Description       = description;
		EstablishmentDate = establishmentDate;
		WebsiteUrl        = websiteUrl;
		LogoUrl           = logoUrl;
	}

	public ElectionServicePoliticalPartyEventJsonData(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy, DateTime lastUpdatedAt, string lastUpdatedBy): base(id, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy)
	{
		Name              = name;
		Description       = description;
		EstablishmentDate = establishmentDate;
		WebsiteUrl        = websiteUrl;
		LogoUrl           = logoUrl;
	}

	public ElectionServicePoliticalPartyEventJsonData(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy, DateTime lastUpdatedAt, string lastUpdatedBy, DateTime deletedAt, string deletedBy): base(id, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy, deletedAt, deletedBy)
	{
		Name              = name;
		Description       = description;
		EstablishmentDate = establishmentDate;
		WebsiteUrl        = websiteUrl;
		LogoUrl           = logoUrl;
	}


	/// <summary>
	/// Creates a new object that holds data came from a CREATE event
	/// </summary>
	public static ElectionServicePoliticalPartyEventJsonData NewForCreateEvent(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy)
	{
		return new ElectionServicePoliticalPartyEventJsonData(id, name, description, establishmentDate, logoUrl, websiteUrl, createdAt, createdBy);
	}

	/// <summary>
	/// Creates a new object that holds data came from a UPDATE event
	/// </summary>
	public static ElectionServicePoliticalPartyEventJsonData NewForUpdateEvent(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy, DateTime lastUpdatedAt, string lastUpdatedBy)
	{
		return new ElectionServicePoliticalPartyEventJsonData(id, name, description, establishmentDate, logoUrl, websiteUrl, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy);
	}

	/// <summary>
	/// Creates a new object that holds data came from a DELETE event
	/// </summary>
	public static ElectionServicePoliticalPartyEventJsonData NewForDeleteEvent(Guid id, string name, string description, DateTime establishmentDate, string logoUrl, string websiteUrl, DateTime createdAt, string createdBy, DateTime lastUpdatedAt, string lastUpdatedBy, DateTime deletedAt, string deletedBy)
	{
		return new ElectionServicePoliticalPartyEventJsonData(id, name, description, establishmentDate, logoUrl, websiteUrl, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy, deletedAt, deletedBy);
	}

}

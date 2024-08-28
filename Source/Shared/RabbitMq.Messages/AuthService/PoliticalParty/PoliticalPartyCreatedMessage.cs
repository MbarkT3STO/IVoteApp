namespace RabbitMq.Messages.AuthService.PoliticalParty;

public class PoliticalPartyCreatedMessage: BaseEventMessage
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime EstablishmentDate { get; set; }
	public string LogoUrl { get; set; }
	public string WebsiteUrl { get; set; }

	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
}

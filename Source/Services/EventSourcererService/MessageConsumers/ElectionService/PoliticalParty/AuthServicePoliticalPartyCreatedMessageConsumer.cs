using EventSourcererService.DATA.Entities.JsonDataTypes.ElectionService;
using RabbitMq.Messages.AuthService.PoliticalParty;

namespace EventSourcererService.MessageConsumers.ElectionService.PoliticalParty;

public class AuthServicePoliticalPartyCreatedMessageConsumer : BaseConsumer<PoliticalPartyCreatedMessage>
{
	public AuthServicePoliticalPartyCreatedMessageConsumer(AppDbContext dbContext, IDeduplicationService deduplicationService) : base(dbContext, deduplicationService)
	{
	}

	protected override async Task ProcessMessage(PoliticalPartyCreatedMessage message)
	{
		var eventData = ElectionServicePoliticalPartyEventJsonData.NewForCreateEvent(message.Id, message.Name, message.Description, message.EstablishmentDate, message.LogoUrl, message.WebsiteUrl, message.CreatedAt, message.CreatedBy);
		var @event = new ElectionServicePoliticalPartyEvent(message.EventId, "Create", DateTime.UtcNow, message.UserId, eventData);

		await _dbContext.ElectionService_PoliticalPartyEvents.AddAsync(@event);
		await _dbContext.SaveChangesAsync();
	}
}

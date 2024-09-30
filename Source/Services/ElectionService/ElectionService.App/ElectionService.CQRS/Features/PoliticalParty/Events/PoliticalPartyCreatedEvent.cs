namespace ElectionService.CQRS.Features.PoliticalParty.Events;

public class PoliticalPartyCreatedEvent : AppEvent<Entities.PoliticalParty>
{
	public Entities.PoliticalParty PoliticalParty { get; }

	private PoliticalPartyCreatedEvent(IEventDetails eventDetails, Entities.PoliticalParty politicalParty) : base(eventDetails, politicalParty) { }
}

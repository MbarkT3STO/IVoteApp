namespace EventSourcererService.DATA.Entities.ElectionService;

/// <summary>
/// Represents a political party event in the Election Service.
/// </summary>
public class ElectionServicePoliticalPartyEvent : Event<ElectionServicePoliticalPartyEventJsonData>
{
	public ElectionServicePoliticalPartyEvent()
	{
	}

	public ElectionServicePoliticalPartyEvent(Guid eventId, string type, DateTime timeStamp, string userId, ElectionServicePoliticalPartyEventJsonData jsonData) : base(eventId, type, timeStamp, userId, jsonData)
	{
	}
}


public class ElectionServicePoliticalPartyEventEntityConfig : IEntityTypeConfiguration<ElectionServicePoliticalPartyEvent>
{
	public void Configure(EntityTypeBuilder<ElectionServicePoliticalPartyEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<ElectionServicePoliticalPartyEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}
}
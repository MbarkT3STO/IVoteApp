namespace EventSourcererService.DATA.Entities.AuthService;

/// <summary>
/// Represents a user event in the Auth Service.
/// </summary>
public class AuthServiceUserEvent : Event<AuthServiceUserEventJsonData>
{
	public AuthServiceUserEvent()
	{
	}

	public AuthServiceUserEvent(Guid eventId, string type, DateTime timeStamp, string userId, AuthServiceUserEventJsonData jsonData) : base(eventId, type, timeStamp, userId, jsonData)
	{
	}
}


public class AuthServiceUserEventEntityConfig : IEntityTypeConfiguration<AuthServiceUserEvent>
{
	public void Configure(EntityTypeBuilder<AuthServiceUserEvent> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		// Configure the Converter for JsonData property
		builder.Property(e => e.JsonData).HasColumnType("jsonb")
		.HasConversion(
			value => JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNamingPolicy = null }),
			value => JsonSerializer.Deserialize<AuthServiceUserEventJsonData>(value, new JsonSerializerOptions { PropertyNamingPolicy = null })
		);
	}
}
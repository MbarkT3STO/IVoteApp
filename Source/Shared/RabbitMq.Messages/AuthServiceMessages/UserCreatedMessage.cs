namespace RabbitMq.Messages.AuthServiceMessages;

/// <summary>
/// Represents a message that is sent when a new user is created in the Authentication Service.
/// </summary>
public class UserCreatedMessage: BaseEventMessage
{
	public required string Id { get; set; }
	public required string UserName { get; set; }
	public required string? FirstName { get; set; }
	public required string? LastName { get; set; }
	public required string RoleId { get; set; }
	public required string RoleName { get; set; }
	public required string Email { get; set; }
	public required DateTime CreatedAt { get; set; }
}

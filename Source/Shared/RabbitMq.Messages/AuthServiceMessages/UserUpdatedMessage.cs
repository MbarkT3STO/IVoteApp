namespace RabbitMq.Messages.AuthServiceMessages;

/// <summary>
/// Represents a message that contains updated user information.
/// </summary>
public class UserUpdatedMessage
{
    public string Id { get; set; }
    public string NewEmail { get; set; }
    public string NewUsername { get; set; }
    public string NewFirstName { get; set; }
    public string NewLastName { get; set; }
    public string NewRole { get; set; }
}
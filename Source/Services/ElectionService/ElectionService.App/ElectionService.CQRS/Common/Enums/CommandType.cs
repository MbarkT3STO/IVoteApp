namespace ElectionService.CQRS.Common.Enums;

/// <summary>
/// Represents the type of the command.
/// </summary>
public enum CommandType
{
	/// <summary>
	/// Represents the create command type.
	/// </summary>
	Create,

	/// <summary>
	/// Represents the update command type.
	/// </summary>
	Update,

	/// <summary>
	/// Represents the delete command type.
	/// </summary>
	Delete
}

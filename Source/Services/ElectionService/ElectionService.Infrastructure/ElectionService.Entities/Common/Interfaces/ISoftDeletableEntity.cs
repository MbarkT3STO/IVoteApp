namespace ElectionService.Entities.Common.Interfaces;

/// <summary>
/// Represents an entity that is soft deletable.
/// </summary>
public interface ISoftDeletableEntity
{
	bool IsDeleted { get; set; }
	DateTime? DeletedAt { get; set; }
	string DeletedBy { get; set; }
}

namespace ElectionService.Entities.Common.Interfaces;

/// <summary>
/// Represents an entity that is auditable.
/// </summary>
public interface IAuditableEntity : ISoftDeletableEntity
{
	DateTime CreatedAt { get; set; }
	string CreatedBy { get; set; }
	DateTime? LastUpdatedAt { get; set; }
	string? LastUpdatedBy { get; set; }
}
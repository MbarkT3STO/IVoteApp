using ElectionService.Entities.Common.Interfaces;

namespace ElectionService.Entities.Common.Implementations;

/// <summary>
/// Represents an entity that contains audit information such as creation date, last update date, and deletion date.
/// </summary>
/// <typeparam name="T">The type of the entity's identifier.</typeparam>
public class AuditableEntity<T> : Entity<T>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
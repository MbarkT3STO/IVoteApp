namespace EventSourcererService.Common;


/// <summary>
/// Represents an abstract class for auditable JSON entities.
/// </summary>
/// <typeparam name="T">The type of the unique identifier.</typeparam>
public abstract class AuditableJsonEntity<T>: JsonEntity<T>
{
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string? LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }


	protected AuditableJsonEntity(T id, DateTime createdAt): base(id)
	{
		CreatedAt = createdAt;
		CreatedBy = string.Empty;
	}

	protected AuditableJsonEntity(T id, DateTime createdAt, string createdBy): base(id)
	{
		CreatedAt = createdAt;
		CreatedBy = createdBy;
	}

	protected AuditableJsonEntity(T id, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy): base(id)
	{
		CreatedAt     = createdAt;
		CreatedBy     = createdBy;
		LastUpdatedAt = lastUpdatedAt;
		LastUpdatedBy = lastUpdatedBy;
	}

	protected AuditableJsonEntity(T id, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, DateTime? deletedAt, string deletedBy): base(id)
	{
		CreatedAt     = createdAt;
		CreatedBy     = createdBy;
		LastUpdatedAt = lastUpdatedAt;
		LastUpdatedBy = lastUpdatedBy;
		IsDeleted     = deletedAt.HasValue;
		DeletedAt     = deletedAt;
		DeletedBy     = deletedBy;
	}

}

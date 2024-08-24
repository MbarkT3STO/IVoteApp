using ElectionService.Entities.Common.Interfaces;

namespace ElectionService.CQRS.Extensions;

public static class AuditableEntityExtensions
{
	/// <summary>
	/// Writes the create audit.
	/// </summary>
	public static void WriteCreateAudit(this IAuditableEntity entity, string userId)
	{
		entity.CreatedAt = DateTime.UtcNow;
		entity.CreatedBy = userId;
	}

	/// <summary>
	/// Writes the update audit.
	/// </summary>
	public static void WriteUpdateAudit(this IAuditableEntity entity, string userId)
	{
		entity.LastUpdatedAt = DateTime.UtcNow;
		entity.LastUpdatedBy = userId;
	}

	/// <summary>
	/// Writes the delete audit.
	/// </summary>
	public static void WriteDeleteAudit(this IAuditableEntity entity, string userId)
	{
		entity.IsDeleted = true;
		entity.DeletedAt = DateTime.UtcNow;
		entity.DeletedBy = userId;
	}
}

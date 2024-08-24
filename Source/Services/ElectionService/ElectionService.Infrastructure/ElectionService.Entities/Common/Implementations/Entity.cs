namespace ElectionService.Entities.Common.Implementations;

/// <summary>
/// Represents a base class for entities with a generic identifier.
/// </summary>
/// <typeparam name="T">The type of the identifier.</typeparam>
public class Entity<T>
{
	public T Id { get; set; }
}
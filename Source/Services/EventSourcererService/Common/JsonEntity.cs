namespace EventSourcererService.Common;

/// <summary>
/// Represents a JSON entity with a unique identifier of type T.
/// </summary>
/// <typeparam name="T">The type of the unique identifier.</typeparam>
public abstract class JsonEntity<T>
{
    public T Id { get; set; }

    protected JsonEntity(T id)
    {
        Id = id;
    }
}

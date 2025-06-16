namespace Application.Domain.Abstractions;

/// <summary>
/// An abstraction for a domain entity definition which must be identified by an Id.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Entity<T>
{
    public T Id { get; init; }

    protected Entity(T id)
    {
        Id = id;
    }
}

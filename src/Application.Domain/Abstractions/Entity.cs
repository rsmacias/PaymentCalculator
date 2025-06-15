namespace Application.Domain.Abstractions;

public abstract class Entity<T>
{
    public T Id { get; init; }

    protected Entity(T id)
    {
        Id = id;
    }
}

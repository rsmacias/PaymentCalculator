namespace Application.Domain.Abstractions;

/// <summary>
/// An abstraction for persistence
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class, IAggregateRoot
{
}

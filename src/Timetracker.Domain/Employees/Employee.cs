using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.Employees;

public sealed class Employee : Entity
{
    public Employee(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Role role,
        DateTime createdOnUtc
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        CreatedOnUtc = createdOnUtc;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
}

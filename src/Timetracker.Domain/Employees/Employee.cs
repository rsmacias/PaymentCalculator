using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.Employees;

public enum Gender : int
{
    Unknown = 0,
    Male = 1,
    Female = 2
}

public sealed class Employee : Entity
{
    public Employee(
        Guid id,
        FirstName firstName,
        LastName lastName,
        DateOnly birthDate,
        Gender gender,
        Role role,
        DateTimeOffset createdOnUtc
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        Role = role;
        CreatedOnUtc = createdOnUtc;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public Role Role { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public DateTimeOffset? UpdatedOnUtc { get; private set; }
}

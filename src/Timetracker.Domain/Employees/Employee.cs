using Acme.Timetracker.Domain.Abstractions;
using Acme.Timetracker.Domain.Departments;

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
        Guid departmentId
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        Role = role;
        DepartmentId = departmentId;
        CreatedOnUtc = DateTimeOffset.UtcNow;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public Role Role { get; private set; }
    public Guid DepartmentId { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public DateTimeOffset? UpdatedOnUtc { get; private set; }
    // Navigation Properties
    public Department Department { get; private set; }
}

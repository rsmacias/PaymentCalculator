using Acme.Timetracker.Domain.Abstractions;
using Acme.Timetracker.Domain.Employees;

namespace Acme.Timetracker.Domain.Departments;

public sealed class Department : Entity
{
    public Department(
        Guid id, 
        string name, 
        string groupName
    ) : base(id)
    {
        Name = name;
        GroupName = groupName;
        CreatedOnUtc = DateTimeOffset.UtcNow;
    }

    public string Name { get; private set; }
    public string GroupName { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public DateTimeOffset? UpdatedOnUtc { get; private set; }
    // Navigation Properties
    public List<Employee> Employees { get; set; }
}

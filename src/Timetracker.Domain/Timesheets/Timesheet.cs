using Acme.Timetracker.Domain.Abstractions;
using Acme.Timetracker.Domain.Employees;
using Acme.Timetracker.Domain.Shared;
using Acme.Timetracker.Domain.WorkItems;

namespace Acme.Timetracker.Domain.Timesheets;

public sealed class Timesheet : Entity
{
    private Timesheet(
        Guid id, 
        Guid workItemId, 
        Guid employeeId,
        WorkCategory category, 
        Description description,
        DateRange duration,
        DateTime createdOnUtc
    ) : base(id)
    {
        WorkItemId = workItemId;
        EmployeeId = employeeId;
        Category = category;
        Duration = duration;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid EmployeeId { get; private set; }
    public Guid WorkItemId { get; private set; }
    public WorkCategory Category { get; private set; }
    public Description Description { get; private set; }
    public DateRange Duration { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Timesheet Register(
        Employee employee, 
        WorkItem workItem, 
        WorkCategory category, 
        Description description,
        DateRange duration,
        DateTime utcNow)
    {
        var timeSheet = new Timesheet(
            Guid.NewGuid(), 
            employee.Id, 
            workItem.Id, 
            category, 
            description,
            duration,
            utcNow
        );

        // TODO: Register domain events

        return timeSheet;
    }
}
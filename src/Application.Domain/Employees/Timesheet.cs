using Application.Domain.Abstractions;
using Application.Domain.Shared;
using FluentResults;

namespace Application.Domain.Employees;

public enum WorkTypes
{
    Meeting = 1,
    Design = 2,
    ProductionSupport = 3,
    QATesting = 4,
    Development = 5,
    DeploymentSupport = 6
}

public sealed class Timesheet : Entity<long>
{
    public long EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public WorkTypes Type { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public string Details { get; private set; }

    private Timesheet(
        long id, 
        Employee employee, 
        WorkTypes type, 
        DateOnly date,
        TimeRange timeRange,
        string details) : base(id)
    {
        Employee = employee;
        EmployeeId = Employee.Id;
        Type = type;
        Date = date;
        TimeRange = timeRange;
        Details = details;
    }

    public static Result<Timesheet> Register(
        Employee employee, 
        WorkTypes type, 
        DateOnly date, 
        TimeRange timeRange,
        string details)
    {
        if (string.IsNullOrWhiteSpace(details))
            return Result.Fail("The task details must be provided.");
        
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (date > today)
            return Result.Fail("The date of the task is not valid.");

        var timesheet = new Timesheet(0L, employee, type, date, timeRange, details);

        return Result.Ok(timesheet);
    }
}

using Application.Domain.Abstractions;
using Application.Domain.PaymentPeriods;
using Application.Domain.Shared;
using FluentResults;

namespace Application.Domain.Employees;

public enum WorkStatus
{
    Applicant = 1,
    Hired = 2,
    Retired = 3
}

public sealed class Employee : Entity<long>, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public WorkStatus Status { get; private set; }

    private readonly List<Timesheet> _reportedWorkingHours = new List<Timesheet>();
    public IReadOnlyList<Timesheet> ReportedWorkingHours => _reportedWorkingHours.AsReadOnly();

    private readonly List<PaymentRole> _payments = new List<PaymentRole>();
    public IReadOnlyList<PaymentRole> Payments => _payments.AsReadOnly();


    public Employee(
        long id, 
        string firstName, 
        string? middleName, 
        string lastName, 
        DateOnly birthDate, 
        WorkStatus status) : base(id)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        BirthDate = birthDate;
        Status = status;
    }

    public Result<Timesheet> ReportTimeSheet(
        WorkTypes type, 
        DateOnly date, 
        TimeRange timeRange, 
        string details)
    {
        if (Status != WorkStatus.Hired)
            return Result.Fail("Currently, the employee is not hired.");

        var timesheetRegistration = Timesheet.Register(this, type, date, timeRange, details);

        if (timesheetRegistration.IsFailed)
            return timesheetRegistration;

        var timesheetReport = timesheetRegistration.Value;

        _reportedWorkingHours.Add(timesheetReport);

        return Result.Ok(timesheetReport);
    }
}

namespace Application.Domain;

public enum WorkTypes
{
    Meeting = 1,
    Design = 2,
    ProductionSupport = 3,
    QATesting = 4,
    Development = 5,
    DeploymentSupport = 6
}

public sealed class Timesheet
{
    public long Id { get; private set; }
    public long EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public WorkTypes Type { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly Start { get; private set; }
    public TimeOnly End { get; private set; }
    public string Details { get; private set; }

    public Timesheet(
        long id, 
        Employee employee, 
        WorkTypes type, 
        DateOnly date, 
        TimeOnly start, 
        TimeOnly end, 
        string details)
    {
        Id = id;
        Employee = employee;
        EmployeeId = Employee.Id;
        Type = type;
        Date = date;
        Start = start;
        End = end;
        Details = details;
    }
}

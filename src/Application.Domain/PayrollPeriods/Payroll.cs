using Application.Domain.Abstractions;
using Application.Domain.Employees;

namespace Application.Domain.PayrollPeriods;

public sealed class Payroll : Entity<long>
{
    public PayrollPeriod Period { get; private set; }
    public long PeriodId { get; private set; }

    public Employee Employee { get; private set; }
    public long EmployeeId { get; private set; }

    public double WorkedHours { get; private set; }
    public double Payment { get; private set; }

    public Payroll(
        long id, 
        PayrollPeriod period, 
        Employee employee, 
        double workedHours, 
        double payment) : base(id)
    {
        Period = period;
        PeriodId = Period.Id;
        Employee = employee;
        EmployeeId = Employee.Id;
        WorkedHours = workedHours;
        Payment = payment;
    }
}

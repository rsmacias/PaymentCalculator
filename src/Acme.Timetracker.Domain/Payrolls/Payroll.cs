using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.Payrolls;

public sealed class Payroll : Entity
{
    public Payroll(Guid id) : base(id)
    {
        
    }

    public Guid EmployeeId { get; private set; }
}
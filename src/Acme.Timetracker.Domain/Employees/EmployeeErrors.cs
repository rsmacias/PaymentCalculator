using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.Employees;

public static class EmployeeErrors
{
    public static Error NotFound = new("Employee.NotFound", "The provided employee does not exist");
}
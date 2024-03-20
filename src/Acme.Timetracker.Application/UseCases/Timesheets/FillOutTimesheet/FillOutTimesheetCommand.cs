using Acme.Timetracker.Application.Abstractions.Messaging;
using Acme.Timetracker.Domain.Shared;
using Acme.Timetracker.Domain.Timesheets;

namespace Acme.Timetracker.Application.UseCases.Timesheets.FillOutTimesheet;

public sealed record FillOutTimesheetCommand
(
    Guid EmployeeId,
    Guid WorkItemId,
    WorkCategory Category,
    Description Description,
    DateTime StartedWorkingTime,
    DateTime EndedWorkingTime
) : ICommand<Guid>;
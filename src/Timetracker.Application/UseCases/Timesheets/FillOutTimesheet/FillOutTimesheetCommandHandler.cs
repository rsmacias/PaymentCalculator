using Acme.Timetracker.Application.Abstractions.Messaging;
using Acme.Timetracker.Domain.Abstractions;
using Acme.Timetracker.Domain.Employees;
using Acme.Timetracker.Domain.Timesheets;
using Acme.Timetracker.Domain.WorkItems;

namespace Acme.Timetracker.Application.UseCases.Timesheets.FillOutTimesheet;

internal sealed class FillOutTimesheetCommandHandler : ICommandHandler<FillOutTimesheetCommand, Guid>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWorkItemRepository _workItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FillOutTimesheetCommandHandler(
        IEmployeeRepository employeeRepository,
        IWorkItemRepository workItemRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _workItemRepository = workItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(FillOutTimesheetCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Failure<Guid>(EmployeeErrors.NotFound);

        var workItem = await _workItemRepository.GetByIdAsync(request.WorkItemId, cancellationToken);
        if(workItem is null)
            return Result.Failure<Guid>(WorkItemErrors.NotFound);

        var duration = DateRange.Create(request.StartedWorkingTime, request.EndedWorkingTime);

        var timesheet = Timesheet.Register(
            employee, 
            workItem, 
            request.Category, 
            request.Description, 
            duration, 
            DateTime.UtcNow
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return timesheet.Id;
    }
}
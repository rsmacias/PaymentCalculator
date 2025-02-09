using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.WorkItems;

public static class WorkItemErrors
{
    public static Error NotFound => new("WorkItem.NotFound", "The provided work item does not exist.");
}
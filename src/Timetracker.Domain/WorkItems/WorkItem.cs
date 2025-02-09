using Acme.Timetracker.Domain.Abstractions;
using Acme.Timetracker.Domain.Shared;

namespace Acme.Timetracker.Domain.WorkItems;

public sealed class WorkItem : Entity
{
    private WorkItem(
        Guid id,
        Name name,
        Description description,
        WorkItemType type,
        DateTime createdOnUtc) : base(id)
    {
        Name = name;
        Description = description;
        CreatedOnUtc = createdOnUtc;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public WorkItemType Type { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }

    public static WorkItem Create(Name name, Description description, WorkItemType type, DateTime utcNow)
    {
        var workItem = new WorkItem(Guid.NewGuid(), name, description, type, utcNow);

        // TODO: Side effects

        return workItem;
    }
}

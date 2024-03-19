namespace Acme.Timetracker.Domain.Timesheets;

public sealed record DateRange
{
    private DateRange()
    {
    }

    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public static DateRange Create(DateTime start, DateTime end)
    {
        if (start > end)
            throw new ArgumentException("End date time precedes the start date time.");

        return new DateRange()
        {
            Start = start,
            End = end
        };
    }

    public double GetHours()
    {
        return End.Subtract(Start).TotalHours;
    }
}
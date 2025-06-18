using Application.Domain.Shared;

namespace Application.Domain.Abstractions;

public static class Extensions
{
    public static bool IsBetween(this DateOnly date, DateRange dateRange)
        => date >= dateRange.Start && date <= dateRange.End;
}

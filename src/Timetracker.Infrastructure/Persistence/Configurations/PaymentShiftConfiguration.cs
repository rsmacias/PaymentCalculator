using Acme.Timetracker.Domain.Shifts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acme.Timetracker.Infrastructure.Persistence.Configurations;

internal sealed class PaymentShiftConfiguration : IEntityTypeConfiguration<PaymentShift>
{
    public void Configure(EntityTypeBuilder<PaymentShift> builder)
    {
        builder.ToTable(nameof(PaymentShift));

        builder.HasKey(x => x.Id);

        builder.Property(ps => ps.Type)
            .HasConversion<int>();

        builder.Property(ps => ps.WeekDay)
            .HasConversion<int>();

        builder.Property(ps => ps.IsFullDay)
            .HasDefaultValue(false);

        builder.Property(ps => ps.StartAt);

        builder.Property(ps => ps.EndAt);

        builder.Property(ps => ps.StartDate)
            .HasDefaultValue(DateOnly.MinValue);

        builder.Property(ps => ps.EndDate)
            .HasDefaultValue(DateOnly.MaxValue);

        builder.Property(ps => ps.IsActive)
            .HasDefaultValue(true);

        builder.Property(ps => ps.CreatedOnUtc)
            .HasDefaultValue(DateTimeOffset.UtcNow);

        builder.Property(ps => ps.UpdateOnUtc);
    }
}

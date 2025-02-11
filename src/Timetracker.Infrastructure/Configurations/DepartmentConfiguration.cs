using Acme.Timetracker.Domain.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acme.Timetracker.Infrastructure.Configurations;

internal sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(50);

        builder.Property(d => d.GroupName)
            .HasMaxLength(50);

        builder.Property(d => d.CreatedOnUtc)
            .HasDefaultValue(DateTimeOffset.UtcNow);

        builder.Property(d => d.UpdatedOnUtc);
    }
}

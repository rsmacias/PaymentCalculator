using Acme.Timetracker.Domain.Departments;
using Acme.Timetracker.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace Acme.Timetracker.Infrastructure;

public class TimetrackerContext : DbContext
{
    public TimetrackerContext(DbContextOptions<TimetrackerContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimetrackerContext).Assembly);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}

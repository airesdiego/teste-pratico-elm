using elumini.domain.Task;
using elumini.domain.TaskStatus;
using Microsoft.EntityFrameworkCore;

namespace elumini.infra.mssql.Context;

public class EfSqlContext : DbContext
{
    public EfSqlContext(DbContextOptions<EfSqlContext> options)
        : base(options)
    {
    }

    public DbSet<Tasks> Task { get; set; }
    public DbSet<Status> Status { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "New" },
            new Status { Id = 2, Name = "In progress" },
            new Status { Id = 3, Name = "Finished" },
            new Status { Id = 4, Name = "Delayed" }
        );
    }
}

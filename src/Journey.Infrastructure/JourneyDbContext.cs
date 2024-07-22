using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Activity = System.Diagnostics.Activity;

namespace Journey.Infrastructure;

public class JourneyDbContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Entities.Activity> Activities { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=JourneyDatabase.db");
    }

}
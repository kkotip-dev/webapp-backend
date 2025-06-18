using Microsoft.EntityFrameworkCore;

using KkotipAppApi.Entities;

namespace KkotipAppApi;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> Tokens { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Call> Calls { get; set; }
    public DbSet<Pair> Pairs { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<JournalEntry> JournalEntries { get; set; }
    public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
    public DbSet<Location> Locations { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                                    $"Port ={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                    $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
                                    $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                                    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}"
                                );
    }
}

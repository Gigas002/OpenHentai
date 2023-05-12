using Microsoft.EntityFrameworkCore;

namespace OpenHentai.Database;

public class DatabaseContext : DbContext
{
    #region Properties

    private readonly StreamWriter _logStream = new StreamWriter("log.txt", true);

    public DbSet<Tag> Tags { get; set; } = null!;

    public string DatabasePath { get; init; } = null!;

    #endregion

    public DatabaseContext(string databasePath = "../openhentai.db") => DatabasePath = databasePath;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}")
                      .UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(_logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Tag>()
        //             .Property(e => e.Description)
        //             .HasConversion(new DescriptionConverter());
    }

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
    }
}

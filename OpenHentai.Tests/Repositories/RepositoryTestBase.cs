using Microsoft.Data.Sqlite;

namespace OpenHentai.Tests.Repositories;

public abstract class RepositoryTestsBase
{
    public const string DatabasePath = ":memory:";

    protected SqliteConnection SqliteConnection { get; } = new($"Data Source={DatabasePath}");

    protected DbContextOptions<DatabaseContext> ContextOptions { get; set; }

    [SetUp]
    public async Task SetupAsync()
    {
        ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(SqliteConnection).Options;

        await SqliteConnection.OpenAsync().ConfigureAwait(false);

        using var db = new DatabaseContext(ContextOptions);

        await db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }

    [TearDown]
    public Task CleanUp() => SqliteConnection.CloseAsync();
}

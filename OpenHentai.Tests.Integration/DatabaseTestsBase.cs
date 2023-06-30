using Microsoft.Data.Sqlite;
using System.Text;

namespace OpenHentai.Tests.Integration;

public abstract class DatabaseTestsBase
{
    public const string DatabasePath = ":memory:";

    protected SqliteConnection SqliteConnection { get; } = new($"Data Source={DatabasePath}");

    protected DbContextOptions<DatabaseContext> ContextOptions { get; set; }

    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(SqliteConnection).Options;

        await SqliteConnection.OpenAsync().ConfigureAwait(false);

        using var db = new DatabaseContext(ContextOptions);

        await db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }

    [OneTimeTearDown]
    public Task CleanUp() => SqliteConnection.CloseAsync();

    protected static async Task<string> SerializeEntityAsync<T>(IEnumerable<T> entity) where T : class
    {
        var options = Essential.JsonSerializerOptions;
        var jsonPath = $"../{typeof(T)}.json";

        var stream = File.OpenWrite(jsonPath);
        await JsonSerializer.SerializeAsync(stream, entity, options).ConfigureAwait(false);

        stream.Close();

        var json = await File.ReadAllTextAsync(jsonPath).ConfigureAwait(false);

        return json;
    }

    protected static async Task<T?> DeserializeEntityAsync<T>(string json) where T : class
    {
        var options = Essential.JsonSerializerOptions;

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var entity = await JsonSerializer.DeserializeAsync<T>(stream, options).ConfigureAwait(false);

        return entity;
    }
}

using Microsoft.Data.Sqlite;
using System.Text;

namespace OpenHentai.Tests.Integration;

public abstract class DatabaseTestsBase
{
    public const string DatabasePath = ":memory:";

    protected static SqliteConnection SqliteConnection { get; } = new($"Data Source={DatabasePath}");

    protected static SqliteConnectionDesiredState ConnectionState { get; set; } = SqliteConnectionDesiredState.OpenNew;

    protected static int CurrentTestOrder { get; set; }

    protected DbContextOptions<DatabaseContext> ContextOptions { get; set; }

    protected static HashSet<TestState> TestStates { get; } = new()
    {
        new(TestKind.PushAuthors, false),
        new(TestKind.PushCircles, false),
        new(TestKind.PushManga, false),
        new(TestKind.PushCharacters, false),
        new(TestKind.PushTags, false),
        new(TestKind.PushTagsRelations, false),
        new(TestKind.PushAuthorsRelations, false),
        new(TestKind.PushCharactersRelations, false),
        new(TestKind.PushCreationsRelations, false),
        new(TestKind.PushAuthorsCircles, false),
        new(TestKind.PushAuthorsCreations, false),
        new(TestKind.PushCharactersCreations, false),
        new(TestKind.PushAuthorsTags, false),
        new(TestKind.PushCharactersTags, false),
        new(TestKind.PushCreationsCircles, false),
        new(TestKind.PushCreationsTags, false),
        new(TestKind.PushCirclesTags, false),
        new(TestKind.ReadAuthors, false),
        new(TestKind.ReadCharacters, false),
        new(TestKind.ReadCircles, false),
        new(TestKind.ReadManga, false),
        new(TestKind.ReadTags, false)
    };

    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(SqliteConnection).Options;

        if (ConnectionState == SqliteConnectionDesiredState.OpenNew || ConnectionState == SqliteConnectionDesiredState.Reopen)
            await SqliteConnection.OpenAsync().ConfigureAwait(false);

        using var db = new DatabaseContext(ContextOptions);

        await db.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }

    [OneTimeTearDown]
    public async Task CleanUp()
    {
        if (ConnectionState == SqliteConnectionDesiredState.Close)
            await SqliteConnection.CloseAsync().ConfigureAwait(false);
    }

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

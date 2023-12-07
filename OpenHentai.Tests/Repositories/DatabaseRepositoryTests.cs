using OpenHentai.Creatures;
using OpenHentai.Repositories;

namespace OpenHentai.Tests.Repositories;

public class DatabaseRepositoryTests : RepositoryTestsBase
{
    [Test]
    public void ConstructorTest()
    {
        var opts = new DbContextOptionsBuilder<DatabaseContext>().Options;

        var contextMock = new Mock<DatabaseContext>(opts);

        using var ar = new AuthorsRepository(contextMock.Object);
    }

    [Test]
    public async Task GetEntryTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);

        db.Authors.Add(author);

        using var ar = new AuthorsRepository(db);

        await ar.SaveChangesAsync();

        var result = await ar.GetEntryAsync<Author>(id);
    }

    [Test]
    public async Task AddEntryTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);

        using var ar = new AuthorsRepository(db);

        await ar.AddEntryAsync(author);

        var result = await ar.GetEntryAsync<Author>(id);
    }

    [Test]
    public async Task RemoveEntryTest1()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);

        await db.Authors.AddAsync(author);
        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.RemoveEntryAsync(author);

        var result = await ar.GetEntryAsync<Author>(id);
    }

    [Test]
    public async Task RemoveEntryTest2()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);

        await db.Authors.AddAsync(author);
        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.RemoveEntryAsync<Author>(id);

        var result = await ar.GetEntryAsync<Author>(id);
    }

    [Test]
    public async Task UpdateEntryTest()
    {
        const ulong id = 1;

        var db = new DatabaseContext(ContextOptions);

        var author1 = new Author(id) { Age = 10 };

        await db.Authors.AddAsync(author1);
        await db.SaveChangesAsync();

        // we have to close current context, since entry with the same id
        // is already being tracked
        await db.DisposeAsync();

        using var db2 = new DatabaseContext(ContextOptions);

        using var ar = new AuthorsRepository(db2);

        var author2 = new Author(id) { Age = 25 };

        await ar.UpdateEntryAsync(id, author2);

        var result = await ar.GetEntryAsync<Author>(id);

        if (result.Age != 25) Assert.Fail();
    }
}

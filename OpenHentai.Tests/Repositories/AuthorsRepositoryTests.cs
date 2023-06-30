using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Repositories;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Repositories;

// TODO: fuck it, I'm giving up on writing tests with Mocks
// hope someone grabs it, since I'm not capable

public class AuthorsRepositoryTests : RepositoryTestsBase
{
    [Test]
    public void ConstructorTest()
    {
        var opts = new DbContextOptionsBuilder<DatabaseContext>().Options;

        var contextMock = new Mock<DatabaseContext>(opts);

        using var ar = new AuthorsRepository(contextMock.Object);
    }

    [Test]
    public void GetAuthorsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(1);

        db.Authors.Add(author);

        db.SaveChanges();

        using var ar = new AuthorsRepository(db);

        var authors = ar.GetAuthors().ToList();
    }

    [Test]
    public void GetAuthorsNamesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authorName = new AuthorsNames(new Author(1), "name", "default");
        var authorsNames = new List<AuthorsNames>() { authorName };

        db.AuthorsNames.AddRange(authorsNames);

        db.SaveChanges();

        using var ar = new AuthorsRepository(db);

        var authorNames = ar.GetAuthorsNames().ToList();
    }

    [Test]
    public async Task GetAuthorNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var authorName = new AuthorsNames(author, "name", "default");
        var authorsNames = new List<AuthorsNames>() { authorName };

        await db.AuthorsNames.AddRangeAsync(authorsNames);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        var authorNames = await ar.GetAuthorNamesAsync(id);
    }

    [Test]
    public async Task GetCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var circle = new Circle(id);

        author.Circles.Add(circle);

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        var circles = await ar.GetCirclesAsync(id);
    }

    [Test]
    public async Task GetCreationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var creation = new Creation(id);
        var ac = new AuthorsCreations(author, creation, AuthorRole.MainPageIllustrator);

        author.Creations.Add(ac);

        await db.AuthorsCreations.AddAsync(ac);

        await db.SaveChangesAsync();

        await using var ar = new AuthorsRepository(db);

        var creations = await ar.GetCreationsAsync(id);
    }

    [Test]
    public async Task AddAuthorNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var authorName = new LanguageSpecificTextInfo("default::name");
        var authorsNames = new HashSet<LanguageSpecificTextInfo>() { authorName };

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.AddAuthorNamesAsync(id, authorsNames);

        var authorNames = await ar.GetAuthorNamesAsync(id);
    }

    [Test]
    public async Task AddCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var circle = new Circle(id);

        await db.Authors.AddAsync(author);
        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.AddCirclesAsync(id, new() { id });

        var circles = await ar.GetCirclesAsync(id);
    }

    [Test]
    public async Task AddCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var manga = new Manga(id);

        await db.Authors.AddAsync(author);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.AddCreationsAsync(id, new() { { id, AuthorRole.Unknown } });

        var creations = await ar.GetCreationsAsync(id);
    }

    [Test]
    public async Task RemoveAuthorNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var authorName = new AuthorsNames(author, "name", "default") { Id = id };
        var authorsNames = new List<AuthorsNames>() { authorName };

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.RemoveAuthorNamesAsync(id, new() { id });

        var names = await ar.GetAuthorNamesAsync(id);
    }

    [Test]
    public async Task RemoveCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var circle = new Circle(id);

        await db.Authors.AddAsync(author);
        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.RemoveCirclesAsync(id, new() { id });

        var circles = await ar.GetCirclesAsync(id);
    }

    [Test]
    public async Task RemoveCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var manga = new Manga(id);

        await db.Authors.AddAsync(author);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var ar = new AuthorsRepository(db);

        await ar.RemoveCreationsAsync(id, new() { id });

        var creations = await ar.GetCreationsAsync(id);
    }
}

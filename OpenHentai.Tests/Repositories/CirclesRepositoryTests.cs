using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Repositories;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Repositories;

public class CirclesRepositoryTests : RepositoryTestsBase
{
    [Test]
    public void ConstructorTest()
    {
        var opts = new DbContextOptionsBuilder<DatabaseContext>().Options;

        var contextMock = new Mock<DatabaseContext>(opts);

        using var cr = new CirclesRepository(contextMock.Object);
    }

    [Test]
    public void GetCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(1);

        db.Circles.Add(circle);

        db.SaveChanges();

        using var cr = new CirclesRepository(db);

        var circles = cr.GetCircles().ToList();
    }

    [Test]
    public void GetAllTitlesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var circleTitle = new CirclesTitles(new Circle(1), "title", "default");
        var circlesTitles = new List<CirclesTitles>() { circleTitle };

        db.CirclesTitles.AddRange(circlesTitles);

        db.SaveChanges();

        using var cr = new CirclesRepository(db);

        var circleTitles = cr.GetAllTitles().ToList();
    }

    [Test]
    public async Task GetTitlesAsync()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var circleTitle = new CirclesTitles(circle, "title", "default");
        var circlesTitles = new List<CirclesTitles>() { circleTitle };

        await db.CirclesTitles.AddRangeAsync(circlesTitles);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        var circleTitles = await cr.GetTitlesAsync(id);
    }

    [Test]
    public async Task GetAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var author = new Author(id);

        circle.Authors.Add(author);

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        var authors = await cr.GetAuthorsAsync(id);
    }

    [Test]
    public async Task GetCreationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var creation = new Creation(id);

        circle.Creations.Add(creation);

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        await using var cr = new CirclesRepository(db);

        var creations = await cr.GetCreationsAsync(id);
    }

    [Test]
    public async Task GetTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var tag = new Tag(id);

        circle.Tags.Add(tag);

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        var tags = await cr.GetTagsAsync(id);
    }

    [Test]
    public async Task AddTitlesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var circleTitle = new LanguageSpecificTextInfo("default::name");
        var circlesTitles = new HashSet<LanguageSpecificTextInfo>() { circleTitle };

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.AddTitlesAsync(id, circlesTitles);

        var circleTitles = await cr.GetTitlesAsync(id);
    }

    [Test]
    public async Task AddAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var author = new Author(id);

        await db.Circles.AddAsync(circle);
        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.AddAuthorsAsync(id, new() { id });

        var authors = await cr.GetAuthorsAsync(id);
    }

    [Test]
    public async Task AddCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var manga = new Manga(id);

        await db.Circles.AddAsync(circle);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.AddCreationsAsync(id, new() { id });

        var creations = await cr.GetCreationsAsync(id);
    }

    [Test]
    public async Task AddTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var tag = new Tag(id);

        await db.Circles.AddAsync(circle);
        await db.Tags.AddAsync(tag);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.AddTagsAsync(id, new() { id });

        var tags = await cr.GetTagsAsync(id);
    }

    [Test]
    public async Task RemoveTitlesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var circleTitle = new CirclesTitles(circle, "name", "default") { Id = id };
        var circlesTitles = new List<CirclesTitles>() { circleTitle };

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.RemoveTitlesAsync(id, new() { id });

        var names = await cr.GetTitlesAsync(id);
    }

    [Test]
    public async Task RemoveAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var author = new Author(id);

        await db.Circles.AddAsync(circle);
        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.RemoveAuthorsAsync(id, new() { id });

        var authors = await cr.GetAuthorsAsync(id);
    }

    [Test]
    public async Task RemoveCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var manga = new Manga(id);

        await db.Circles.AddAsync(circle);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.RemoveCreationsAsync(id, new() { id });

        var creations = await cr.GetCreationsAsync(id);
    }

    [Test]
    public async Task RemoveTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var circle = new Circle(id);
        var tag = new Tag(id);

        circle.Tags.Add(tag);

        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using var cr = new CirclesRepository(db);

        await cr.RemoveTagsAsync(id, new() { id });

        var tags = await cr.GetTagsAsync(id);
    }
}

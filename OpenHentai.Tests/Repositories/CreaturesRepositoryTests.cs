using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Repositories;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Repositories;

public class CreaturesRepositoryTests : RepositoryTestsBase
{
    [Test]
    public async Task GetNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var name = new CreaturesNames(new Author(id), "name", "default");
        var names = new List<CreaturesNames>() { name };

        db.CreaturesNames.AddRange(names);

        db.SaveChanges();

        using IAuthorsRepository ar = new AuthorsRepository(db);

        var result = await ar.GetNamesAsync(id);
    }

    [Test]
    public async Task GetTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var tag = new Tag(id);

        author.Tags.Add(tag);

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        await using IAuthorsRepository ar = new AuthorsRepository(db);

        var tags = await ar.GetTagsAsync(id);
    }

    [Test]
    public async Task GetRelationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var author1 = new Author(id);
        var author2 = new Author(id + 1);

        var cr = new CreaturesRelations(author1, author2, Relations.CreatureRelations.Unknown);

        await db.CreaturesRelations.AddAsync(cr);

        await db.SaveChangesAsync();

        await using IAuthorsRepository ar = new AuthorsRepository(db);

        var relations = await ar.GetRelationsAsync(id);
    }

    [Test]
    public async Task AddNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var name = new LanguageSpecificTextInfo("default::name");
        var names = new HashSet<LanguageSpecificTextInfo>() { name };

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using IAuthorsRepository ar = new AuthorsRepository(db);

        await ar.AddNamesAsync(id, names);

        var result = await ar.GetNamesAsync(id);
    }

    [Test]
    public async Task AddTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var tag = new Tag(id);

        await db.Authors.AddAsync(author);
        await db.Tags.AddAsync(tag);

        await db.SaveChangesAsync();

        using IAuthorsRepository ar = new AuthorsRepository(db);

        await ar.AddTagsAsync(id, [id]);

        var tags = await ar.GetTagsAsync(id);
    }

    [Test]
    public async Task AddRelationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author1 = new Author(id);
        var author2 = new Author(id + 1);

        await db.Authors.AddRangeAsync(author1, author2);

        await db.SaveChangesAsync();

        using IAuthorsRepository ar = new AuthorsRepository(db);

        await ar.AddRelationsAsync(id, new() { { id + 1, Relations.CreatureRelations.Unknown } });

        var relations = await ar.GetRelationsAsync(id);
    }

    [Test]
    public async Task RemoveNamesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var name = new CreaturesNames(new Author(id), "name", "default");
        var names = new List<CreaturesNames>() { name };

        db.CreaturesNames.AddRange(names);

        db.SaveChanges();

        using IAuthorsRepository ar = new AuthorsRepository(db);

        var result = await ar.RemoveNamesAsync(id, [id]);
    }

    [Test]
    public async Task RemoveTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var author = new Author(id);
        var tag = new Tag(id);

        author.Tags.Add(tag);

        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        await using IAuthorsRepository ar = new AuthorsRepository(db);

        var result = await ar.RemoveTagsAsync(id, [id]);
    }

    [Test]
    public async Task RemoveRelationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var author1 = new Author(id);
        var author2 = new Author(id + 1);

        var cr = new CreaturesRelations(author1, author2, Relations.CreatureRelations.Unknown);

        await db.CreaturesRelations.AddAsync(cr);

        await db.SaveChangesAsync();

        await using IAuthorsRepository ar = new AuthorsRepository(db);

        var result = await ar.RemoveRelationsAsync(id, [id]);
    }
}

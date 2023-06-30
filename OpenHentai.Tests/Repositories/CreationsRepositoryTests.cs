using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Repositories;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Repositories;

public class CreationsRepositoryTests : RepositoryTestsBase
{
    [Test]
    public async Task GetTitlesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var title = new CreationsTitles(new Manga(id), "name", "default");
        var titles = new List<CreationsTitles>() { title };

        db.CreationsTitles.AddRange(titles);

        db.SaveChanges();

        using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.GetTitlesAsync(id);
    }

    [Test]
    public async Task GetAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var author = new Author(id);

        manga.AddAuthor(author, Roles.AuthorRole.MainArtist);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        var authors = await mr.GetAuthorsAsync(id);
    }

    [Test]
    public async Task GetCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var circle = new Circle(id);

        manga.Circles.Add(circle);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        var circles = await mr.GetCirclesAsync(id);
    }

    [Test]
    public async Task GetRelationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var manga1 = new Manga(id);
        var manga2 = new Manga(id + 1);

        var cr = new CreationsRelations(manga1, manga2, Relations.CreationRelations.Unknown);

        await db.CreationsRelations.AddAsync(cr);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var relations = await mr.GetRelationsAsync(id);
    }

    [Test]
    public async Task GetCharactersTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var character = new Character(id);

        var cc = new CreationsCharacters(manga, character, Roles.CharacterRole.Main);

        await db.CreationsCharacters.AddAsync(cc);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var characters = await mr.GetCharactersAsync(id);
    }

    [Test]
    public async Task GetTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var tag = new Tag(id);

        manga.Tags.Add(tag);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var tags = await mr.GetTagsAsync(id);
    }

    [Test]
    public async Task AddTitlesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var title = new LanguageSpecificTextInfo("default::name");
        var titles = new HashSet<LanguageSpecificTextInfo>() { title };

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddTitlesAsync(id, titles);

        var result = await mr.GetTitlesAsync(id);
    }

    [Test]
    public async Task AddAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var author = new Author(id);

        await db.Manga.AddAsync(manga);
        await db.Authors.AddAsync(author);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddAuthorsAsync(id, new() { { id, Roles.AuthorRole.Unknown } });

        var authors = await mr.GetAuthorsAsync(id);
    }

    [Test]
    public async Task AddCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var circle = new Circle(id);

        await db.Manga.AddAsync(manga);
        await db.Circles.AddAsync(circle);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddCirclesAsync(id, new() { id });

        var circles = await mr.GetCirclesAsync(id);
    }

    [Test]
    public async Task AddRelationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga1 = new Manga(id);
        var manga2 = new Manga(id + 1);

        await db.Manga.AddRangeAsync(manga1, manga2);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddRelationsAsync(id, new() { { id + 1, Relations.CreationRelations.Unknown } });

        var relations = await mr.GetRelationsAsync(id);
    }

    [Test]
    public async Task AddCharactersTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var charatcer = new Character(id);

        await db.Manga.AddAsync(manga);
        await db.Characters.AddAsync(charatcer);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddCharactersAsync(id, new() { { id, Roles.CharacterRole.Unknown } });

        var characters = await mr.GetCharactersAsync(id);
    }

    [Test]
    public async Task AddTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var tag = new Tag(id);

        await db.Manga.AddAsync(manga);
        await db.Tags.AddAsync(tag);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        await mr.AddTagsAsync(id, new() { id });

        var tags = await mr.GetTagsAsync(id);
    }

    [Test]
    public async Task RemoveTitlesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var title = new CreationsTitles(new Manga(id), "name", "default");
        var titles = new List<CreationsTitles>() { title };

        db.CreationsTitles.AddRange(titles);

        db.SaveChanges();

        using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveTitlesAsync(id, new() { id });
    }

    [Test]
    public async Task RemoveAuthorsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var author = new Author(id);

        manga.AddAuthor(author, Roles.AuthorRole.MainArtist);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveAuthorsAsync(id, new() { id });
    }

    [Test]
    public async Task RemoveCirclesTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var circle = new Circle(id);

        manga.Circles.Add(circle);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveCirclesAsync(id, new() { id });
    }

    [Test]
    public async Task RemoveRelationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var manga1 = new Manga(id);
        var manga2 = new Manga(id + 1);

        var cr = new CreationsRelations(manga1, manga2, Relations.CreationRelations.Unknown);

        await db.CreationsRelations.AddAsync(cr);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveRelationsAsync(id, new() { id });
    }

    [Test]
    public async Task RemoveCharactersTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var character = new Character(id);

        var cc = new CreationsCharacters(manga, character, Roles.CharacterRole.Main);

        await db.CreationsCharacters.AddAsync(cc);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveCharactersAsync(id, new() { id });
    }

    [Test]
    public async Task RemoveTagsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var manga = new Manga(id);
        var tag = new Tag(id);

        manga.Tags.Add(tag);

        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        await using IMangaRepository mr = new MangaRepository(db);

        var result = await mr.RemoveTagsAsync(id, new() { id });
    }
}

using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Repositories;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Repositories;

public class CharactersRepositoryTests : RepositoryTestsBase
{
    [Test]
    public void ConstructorTest()
    {
        var opts = new DbContextOptionsBuilder<DatabaseContext>().Options;

        var contextMock = new Mock<DatabaseContext>(opts);

        using var cr = new CharactersRepository(contextMock.Object);
    }

    [Test]
    public void GetCharactersTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var character = new Character(1);

        db.Characters.Add(character);

        db.SaveChanges();

        using var cr = new CharactersRepository(db);

        var characters = cr.GetCharacters().ToList();
    }

    [Test]
    public async Task GetCreationsTest()
    {
        const ulong id = 1;

        await using var db = new DatabaseContext(ContextOptions);

        var character = new Character(id);
        var creation = new Creation(id);
        var cc = new CreationsCharacters(creation, character, CharacterRole.Cosplay);

        character.Creations.Add(cc);

        await db.CreationsCharacters.AddAsync(cc);

        await db.SaveChangesAsync();

        await using var cr = new CharactersRepository(db);

        var creations = await cr.GetCreationsAsync(id);
    }

    [Test]
    public async Task AddCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var character = new Character(id);
        var manga = new Manga(id);

        await db.Characters.AddAsync(character);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var cr = new CharactersRepository(db);

        await cr.AddCreationsAsync(id, new() { { id, CharacterRole.Unknown } });

        var creations = await cr.GetCreationsAsync(id);
    }

    [Test]
    public async Task RemoveCreationsTest()
    {
        const ulong id = 1;

        using var db = new DatabaseContext(ContextOptions);

        var character = new Character(id);
        var manga = new Manga(id);

        await db.Characters.AddAsync(character);
        await db.Manga.AddAsync(manga);

        await db.SaveChangesAsync();

        using var cr = new CharactersRepository(db);

        await cr.RemoveCreationsAsync(id, new() { id });

        var creations = await cr.GetCreationsAsync(id);
    }
}

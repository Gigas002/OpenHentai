using OpenHentai.Creatures;
using OpenHentai.Relations;

#pragma warning disable CA2007

namespace OpenHentai.Tests.Integration;

public class CharactersTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public async Task PushCharactersTest()
    {
        await using var db = new DatabaseContext(ContextOptions);

        var ymM1M = new Mock<Character>("default::Unnamed male");
        var ymM2F = new Mock<Character>("default::Akaname");
        var aM1F = new Mock<Character>("default::Ajax");
        var aM2F = new Mock<Character>("default::Aliza");

        await db.Characters.AddRangeAsync(ymM1M.Object, ymM2F.Object, aM1F.Object, aM2F.Object)
            .ConfigureAwait(false);

        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    [Test]
    [Order(1)]
    public void PushCharactersRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1M = new Mock<Character>("default::Unnamed male");
        var ymM2F = new Mock<Character>("default::Akaname");
        var aM1F = new Mock<Character>("default::Ajax");
        var aM2F = new Mock<Character>("default::Aliza");

        ymM1M.Object.AddRelation(ymM2F.Object, CreatureRelations.Unknown);
        aM1F.Object.AddRelation(aM2F.Object, CreatureRelations.Enemy);

        db.SaveChanges();
    }

    [Test]
    [Order(2)]
    public async Task ReadCharactersTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var characters = db.Characters.Include(c => c.Names)
                                      .Include(c => c.Creations)
                                      .ThenInclude(cc => cc.Origin)
                                      .Include(c => c.Tags)
                                      .Include(c => c.Relations)
                                      .ThenInclude(cr => cr.Related)
                                      .ToList();

        var json = await SerializeEntityAsync(characters).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Character>>(json).ConfigureAwait(false);
    }
}
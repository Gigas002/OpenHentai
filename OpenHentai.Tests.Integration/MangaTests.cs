using Moq;
using OpenHentai.Creations;
using OpenHentai.Relations;

#pragma warning disable CA2007

namespace OpenHentai.Tests.Integration;

public class MangaTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public async Task PushMangaTest()
    {
        await using var db = new DatabaseContext(ContextOptions);

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        await db.Manga.AddRangeAsync(ymM1.Object, ymM2.Object, aM1.Object, aM2.Object)
            .ConfigureAwait(false);

        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    [Test]
    [Order(1)]
    public void PushCreationsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        ymM1.Object.AddRelation(ymM2.Object, CreationRelations.Slave);
        ymM2.Object.AddRelation(ymM1.Object, CreationRelations.Master);
        aM1.Object.AddRelation(aM2.Object, CreationRelations.Parent);
        aM2.Object.AddRelation(aM1.Object, CreationRelations.Child);

        db.SaveChanges();
    }

    [Test]
    [Order(10)]
    public async Task ReadMangaTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var manga = db.Manga.Include(m => m.Relations)
                            .Include(m => m.Titles)
                            .Include(m => m.Authors)
                            .ThenInclude(ac => ac.Origin)
                            .Include(m => m.Circles)
                            .Include(m => m.Characters)
                            .ThenInclude(cc => cc.Related)
                            .Include(m => m.Tags)
                            .ToList();

        var json = await SerializeEntityAsync(manga).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Manga>>(json).ConfigureAwait(false);
    }
}

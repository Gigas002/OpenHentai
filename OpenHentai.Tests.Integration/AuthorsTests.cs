using OpenHentai.Creatures;
using OpenHentai.Relations;

#pragma warning disable CA2007

namespace OpenHentai.Tests.Integration;

public class AuthorsTests : DatabaseTestsBase
{
    #region Push tests

    [Test]
    [Order(1)]
    public async Task PushAuthorsTest()
    {
        await using var db = new DatabaseContext(ContextOptions);

        var ym = new Mock<Author>("default::Yukino Minato");
        var asanagi = new Mock<Author>("default::Asanagi");

        await db.Authors.AddRangeAsync(ym.Object, asanagi.Object).ConfigureAwait(false);

        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    [Test]
    [Order(1)]
    public void PushAuthorsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ym = new Mock<Author>("default::Yukino Minato");
        var asanagi = new Mock<Author>("default::Asanagi");

        ym.Object.AddRelation(asanagi.Object, CreatureRelations.Unknown);
        asanagi.Object.AddRelation(ym.Object, CreatureRelations.Friend);

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(2)]
    public async Task ReadAuthorsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames)
            .Include(a => a.Circles)
            .Include(a => a.Creations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.Names)
            .Include(a => a.Tags)
            .Include(a => a.Relations)
            .ThenInclude(cr => cr.Related);

        var json = await SerializeEntityAsync(authors).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Author>>(json).ConfigureAwait(false);
    }

    #endregion
}

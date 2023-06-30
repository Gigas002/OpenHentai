using Moq;
using OpenHentai.Circles;

#pragma warning disable CA2007

namespace OpenHentai.Tests.Integration;

public class CirclesTests : DatabaseTestsBase
{
    #region Push tests

    [Test]
    [Order(1)]
    public async Task PushCirclesTest()
    {
        await using var db = new DatabaseContext(ContextOptions);

        var nnnt = new Mock<Circle>("default::noraneko-no-tama");
        var fatalpulse = new Mock<Circle>("default::Fatalpulse");

        await db.Circles.AddRangeAsync(nnnt.Object, fatalpulse.Object).ConfigureAwait(false);

        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    #endregion

    #region Read tests

    [Test]
    [Order(2)]
    public async Task ReadCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var circles = db.Circles.Include(c => c.Titles)
                                .Include(c => c.Authors)
                                .Include(c => c.Creations)
                                .Include(c => c.Tags)
                                .ToList();

        var json = await SerializeEntityAsync(circles).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Circle>>(json).ConfigureAwait(false);
    }

    #endregion
}

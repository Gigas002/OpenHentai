using OpenHentai.Circles;

namespace OpenHentai.Tests.Integration;

public class CirclesTests : DatabaseTestsBase
{
    #region Push tests

    [Test]
    [Order(1)]
    public void PushCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var nnntCircle = new Circle("default::noraneko-no-tama");
        nnntCircle.AddTitle("ja-JP::ノラネコノタマ");

        var fCircle = new Circle("default::Fatalpulse");

        db.Circles.AddRange(nnntCircle, fCircle);

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(10)]
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

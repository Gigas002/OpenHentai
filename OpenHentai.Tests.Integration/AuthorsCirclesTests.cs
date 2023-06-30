using OpenHentai.Circles;
using OpenHentai.Creatures;

namespace OpenHentai.Tests.Integration;

public class AuthorsCirclesTests : DatabaseTestsBase
{
    [Test]
    public void PushAuthorsCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ym = new Mock<Author>("default::Yukino Minato");
        var asanagi = new Mock<Author>("default::Asanagi");

        var nnnt = new Mock<Circle>("default::noraneko-no-tama");
        var fatalpulse = new Mock<Circle>("default::Fatalpulse");

        ym.Object.Circles.Add(nnnt.Object);
        fatalpulse.Object.Authors.Add(asanagi.Object);

        db.SaveChanges();
    }
}

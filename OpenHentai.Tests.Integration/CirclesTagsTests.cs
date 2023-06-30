using OpenHentai.Circles;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Integration;

public class CirclesTagsTests : DatabaseTestsBase
{
    [Test]
    public void PushCirclesTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var nnnt = new Mock<Circle>("default::noraneko-no-tama");
        var fatalpulse = new Mock<Circle>("default::Fatalpulse");

        var loliTag = new Mock<Tag>(TagCategory.BodyType, "Loli");
        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var gfTag = new Mock<Tag>(TagCategory.Parody, "Granblue Fantasy");

        nnnt.Object.Tags.Add(loliTag.Object);
        loliTag.Object.Circles.Add(fatalpulse.Object);
        alTag.Object.Circles.Add(fatalpulse.Object);
        gfTag.Object.Circles.Add(fatalpulse.Object);

        db.SaveChanges();
    }
}

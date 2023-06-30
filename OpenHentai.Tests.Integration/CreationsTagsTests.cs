using Moq;
using OpenHentai.Creations;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Integration;

public class CreationsTagsTests : DatabaseTestsBase
{
    [Test]
    public void PushCreationsTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        var loliTag = new Mock<Tag>(TagCategory.BodyType, "Loli");
        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var gfTag = new Mock<Tag>(TagCategory.Parody, "Granblue Fantasy");

        ymM1.Object.Tags.Add(loliTag.Object);
        ymM2.Object.Tags.Add(loliTag.Object);
        loliTag.Object.Creations.Add(aM1.Object);
        alTag.Object.Creations.Add(aM1.Object);
        gfTag.Object.Creations.Add(aM2.Object);

        db.SaveChanges();
    }
}
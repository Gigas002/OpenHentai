using Moq;
using OpenHentai.Creatures;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Integration;

public class CharactersTagsTests : DatabaseTestsBase
{
    [Test]
    public void PushCharactersTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM2F = new Mock<Character>("default::Akaname");
        var aM1F = new Mock<Character>("default::Ajax");
        var aM2F = new Mock<Character>("default::Aliza");

        var loliTag = new Mock<Tag>(TagCategory.BodyType, "Loli");
        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var gfTag = new Mock<Tag>(TagCategory.Parody, "Granblue Fantasy");

        ymM2F.Object.Tags.Add(loliTag.Object);
        loliTag.Object.Creatures.Add(aM1F.Object);
        alTag.Object.Creatures.Add(aM1F.Object);
        gfTag.Object.Creatures.Add(aM2F.Object);

        db.SaveChanges();
    }
}

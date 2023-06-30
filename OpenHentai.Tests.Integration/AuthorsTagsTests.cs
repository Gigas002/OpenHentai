using Moq;
using OpenHentai.Creatures;
using OpenHentai.Tags;

namespace OpenHentai.Tests.Integration;

public class AuthorsTagsTests : DatabaseTestsBase
{
    [Test]
    public void PushAuthorsTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ym = new Mock<Author>("default::Yukino Minato");
        var asanagi = new Mock<Author>("default::Asanagi");

        var loliTag = new Mock<Tag>(TagCategory.BodyType, "Loli");
        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var gfTag = new Mock<Tag>(TagCategory.Parody, "Granblue Fantasy");

        ym.Object.Tags.Add(loliTag.Object);
        alTag.Object.Creatures.Add(asanagi.Object);
        gfTag.Object.Creatures.Add(asanagi.Object);

        db.SaveChanges();
    }
}

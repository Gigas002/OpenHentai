using OpenHentai.Tags;

#pragma warning disable CA2007

namespace OpenHentai.Tests.Integration;

public class TagsTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public async Task PushTagsTest()
    {
        await using var db = new DatabaseContext(ContextOptions);

        var loliTag = new Mock<Tag>(TagCategory.BodyType, "Loli");
        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var gfTag = new Mock<Tag>(TagCategory.Parody, "Granblue Fantasy");
        var al2Tag = new Mock<Tag>(TagCategory.Parody, "azurlane");

        await db.Tags.AddRangeAsync(loliTag.Object, alTag.Object, gfTag.Object, al2Tag.Object)
            .ConfigureAwait(false);

        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    [Test]
    [Order(1)]
    public void PushTagsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var alTag = new Mock<Tag>(TagCategory.Parody, "Azur Lane");
        var al2Tag = new Mock<Tag>(TagCategory.Parody, "azurlane");

        al2Tag.Object.Master = alTag.Object;

        db.SaveChanges();
    }

    [Test]
    [Order(2)]
    public async Task ReadTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var tags = db.Tags.Include(t => t.Creatures)
                          .Include(t => t.Circles)
                          .Include(t => t.Creations)
                          .ToList();

        var json = await SerializeEntityAsync(tags).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Tag>>(json).ConfigureAwait(false);
    }
}
using OpenHentai.Tags;

namespace OpenHentai.Tests.Integration;

public class TagsTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public void PushTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        // init tag for basic categories

        // author (related works), circle, character, manga
        var loliTag = new Tag(TagCategory.BodyType, "Loli");
        loliTag.Description.Add(new("en-US::Little girl"));
        loliTag.Description.Add(new("ru-RU::Маленькая девочка"));

        // character, manga
        var alTag = new Tag(TagCategory.Parody, "Azur Lane");
        alTag.Description.Add(new("en-US::Azur Lane parody tag"));

        // character, manga
        var gfTag = new Tag(TagCategory.Parody, "Granblue Fantasy");
        gfTag.Description.Add(new("en-US::Granblue Fantasy tag"));

        var al2Tag = new Tag(TagCategory.Parody, "azurlane");
        al2Tag.Description.Add(new("en-US::Alias for Azur Lane tag"));

        db.Tags.AddRange(loliTag, alTag, gfTag, al2Tag);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushTagsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var tags = db.Tags.ToHashSet();

        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var al2Tag = tags.FirstOrDefault(t => t.Value == "azurlane");

        al2Tag!.Master = alTag;

        db.SaveChanges();
    }

    [Test]
    [Order(10)]
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
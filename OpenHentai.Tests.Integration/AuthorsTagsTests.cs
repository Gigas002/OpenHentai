namespace OpenHentai.Tests.Integration;

public class AuthorsTagsTests : DatabaseTestsBase
{
    // depends on PushTagsTest(1)
    // depends on PushAuthorsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var tags = db.Tags.Include(tag => tag.Creatures).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        ym!.Tags.Add(loliTag!);
        alTag!.Creatures.Add(asanagi!);
        gfTag!.Creatures.Add(asanagi!);

        db.SaveChanges();
    }
}

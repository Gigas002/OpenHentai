namespace OpenHentai.Tests.Integration;

public class CreationsTagsTests : DatabaseTestsBase
{
    // depends on PushMangaTest(1)
    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        var tags = db.Tags.Include(tag => tag.Creations).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        ymM1!.Tags.Add(loliTag!);
        ymM2!.Tags.Add(loliTag!);
        loliTag!.Creations.Add(aM1!);
        alTag!.Creations.Add(aM1!);
        gfTag!.Creations.Add(aM2!);

        db.SaveChanges();
    }
}
namespace OpenHentai.Tests.Integration;

public class CirclesTagsTests : DatabaseTestsBase
{
    // depends on PushCirclesTest(1)
    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCirclesTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var circles = db.Circles.Include(c => c.Titles).Include(circle => circle.Creations).ToHashSet();

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        var tags = db.Tags.Include(tag => tag.Circles).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        nnnt!.Tags.Add(loliTag!);
        loliTag!.Circles.Add(fatalpulse!);
        alTag!.Circles.Add(fatalpulse!);
        gfTag!.Circles.Add(fatalpulse!);

        db.SaveChanges();
    }
}

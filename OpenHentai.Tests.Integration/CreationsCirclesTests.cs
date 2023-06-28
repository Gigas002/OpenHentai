namespace OpenHentai.Tests.Integration;

public class CreationsCirclesTests : DatabaseTestsBase
{
    // depends on PushMangaTest(1)
    // depends on PushCirclesTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        var circles = db.Circles.Include(c => c.Titles).Include(circle => circle.Creations).ToHashSet();

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        ymM1!.Circles.Add(nnnt!);
        ymM2!.Circles.Add(nnnt!);
        fatalpulse!.Creations.Add(aM1!);
        fatalpulse.Creations.Add(aM2!);

        db.SaveChanges();
    }
}

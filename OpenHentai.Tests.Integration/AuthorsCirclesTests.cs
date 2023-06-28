using NUnit.Framework.Internal;

namespace OpenHentai.Tests.Integration;

public class AuthorsCirclesTests : DatabaseTestsBase
{
    // depends on PushAuthorsTest(1)
    // depends on PushCirclesTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsCirclesTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var circles = db.Circles.Include(c => c.Titles).ToHashSet();

        // or by searching through relative table:
        // var cts = db.CirclesTitles.ToHashSet();
        // var nnntCt = cts.FirstOrDefault(ct => ct.Text == "noraneko-no-tama");
        // var nnnt = circles.FirstOrDefault(c => c.Id == nnntCt.Id);

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        ym!.Circles.Add(nnnt!);
        fatalpulse!.Authors.Add(asanagi!);

        db.SaveChanges();
    }
}
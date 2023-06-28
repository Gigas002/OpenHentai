using OpenHentai.Roles;

namespace OpenHentai.Tests.Integration;

public class AuthorsCreationsTests : DatabaseTestsBase
{
    // depends on PushAuthorsTest(1)
    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsCreationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        ym!.AddCreation(ymM1!, AuthorRole.MainArtist);
        ym.AddCreation(ymM2!, AuthorRole.MainArtist);
        aM1!.AddAuthor(asanagi!, AuthorRole.MainArtist);
        aM2!.AddAuthor(asanagi!, AuthorRole.MainArtist);

        db.SaveChanges();
    }
}

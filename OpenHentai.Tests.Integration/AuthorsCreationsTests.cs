using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Integration;

public class AuthorsCreationsTests : DatabaseTestsBase
{
    [Test]
    public void PushAuthorsCreationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ym = new Mock<Author>("default::Yukino Minato");
        var asanagi = new Mock<Author>("default::Asanagi");

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        ym.Object.AddCreation(ymM1.Object, AuthorRole.MainArtist);
        ym.Object.AddCreation(ymM2.Object, AuthorRole.MainArtist);
        aM1.Object.AddAuthor(asanagi.Object, AuthorRole.MainArtist);
        aM2.Object.AddAuthor(asanagi.Object, AuthorRole.MainArtist);

        db.SaveChanges();
    }
}

using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests;

public class MangaTests
{
    [Test]
    public void ConstructorTest()
    {
        var manga1 = new Manga();
        var manga2 = new Manga(2);

        var namedManga1 = new Manga("default::title 1");

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title 2");
        var namedCircle2 = new Manga(titleMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var manga = new Manga
        {
            Length = 0,
            Volumes = 0,
            Chapters = 0,
            HasImages = true
        };

        var ciMock = new Mock<ColoredInfo>();

        manga.ColoredInfo.Add(ciMock.Object);
    }
}

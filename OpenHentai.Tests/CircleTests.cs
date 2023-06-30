using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Tests;

public class CircleTests
{
    [Test]
    public void ConstructorTest()
    {
        var circle1 = new Circle();
        var circle2 = new Circle(2);
        var namedCircle1 = new Circle("default::name 1");

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name 2");
        var namedCircle2 = new Circle(nameMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var circle = new Circle
        {
            Id = 0
        };

        var ctMock = new Mock<CirclesTitles>();
        var authorMock = new Mock<Author>();
        var mangaMock = new Mock<Manga>();
        var tagMock = new Mock<Tag>();

        circle.Titles.Add(ctMock.Object);
        circle.Authors.Add(authorMock.Object);
        circle.Creations.Add(mangaMock.Object);
        circle.Tags.Add(tagMock.Object);
    }

    [Test]
    public void GetTitlesTest()
    {
        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");
        var circle = new Circle(titleMock.Object);

        var titles = circle.GetTitles();

        var title = titles.FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitlesTest()
    {
        var circle = new Circle();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");

        circle.AddTitles(new List<LanguageSpecificTextInfo> { titleMock.Object});

        var title = circle.GetTitles().FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitleTest()
    {
        var circle = new Circle();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title1");

        circle.AddTitle(titleMock.Object);
        circle.AddTitle("default::title2");

        var titles = circle.GetTitles();

        if (titles is null || !titles.Any())
            Assert.Fail();
    }
}

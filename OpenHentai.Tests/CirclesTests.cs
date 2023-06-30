using Moq;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Tests;

public class CirclesTests
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
        var circle = new Circle("default::title");

        var titles = circle.GetTitles();

        var title = titles.FirstOrDefault(t => t.Language == "default" && t.Text == "title");

        if (title is null)
            Assert.Fail();
    }
}

using System.Globalization;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class CirclesTitlesTests
{
    [Test]
    public void ConstructorTest()
    {
        var circleMock = new Mock<Circle>();
        var textMock = new Mock<LanguageSpecificTextInfo>();

        var ct1 = new CirclesTitles();
        var ct2 = new CirclesTitles(circleMock.Object, textMock.Object);
        var ct3 = new CirclesTitles(circleMock.Object, "name", "default");
        var ct4 = new CirclesTitles(circleMock.Object, "name", CultureInfo.InvariantCulture);
    }

    [Test]
    public void PropertiesTest()
    {
        var circleMock = new Mock<Circle>();

        var ct = new CirclesTitles
        {
            Id = 1,
            Entity = circleMock.Object,
            Text = "name",
            Language = "default"
        };
    }

    [Test]
    public void GetLanguageSpecificTextInfoTest()
    {
        const string str = "default::name";

        var circleMock = new Mock<Circle>();

        var ct = new CirclesTitles(circleMock.Object, new(str));

        var lsti = ct.GetLanguageSpecificTextInfo();

        if (!str.Equals(lsti.ToString(), StringComparison.Ordinal))
            Assert.Fail();
    }
}
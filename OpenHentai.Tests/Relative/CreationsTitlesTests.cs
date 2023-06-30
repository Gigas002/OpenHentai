using System.Globalization;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class CreationsTitlesTests
{
    [Test]
    public void ConstructorTest()
    {
        var creationMock = new Mock<Creation>();
        var textMock = new Mock<LanguageSpecificTextInfo>();

        var ct1 = new CreationsTitles();
        var ct2 = new CreationsTitles(creationMock.Object, textMock.Object);
        var ct3 = new CreationsTitles(creationMock.Object, "name", "default");
        var ct4 = new CreationsTitles(creationMock.Object, "name", CultureInfo.InvariantCulture);
    }

    [Test]
    public void PropertiesTest()
    {
        var creationMock = new Mock<Creation>();

        var ct = new CreationsTitles
        {
            Id = 1,
            Entity = creationMock.Object,
            Text = "name",
            Language = "default"
        };
    }

    [Test]
    public void GetLanguageSpecificTextInfoTest()
    {
        const string str = "default::name";

        var creationMock = new Mock<Creation>();

        var ct = new CreationsTitles(creationMock.Object, new(str));

        var lsti = ct.GetLanguageSpecificTextInfo();

        if (!str.Equals(lsti.ToString(), StringComparison.Ordinal))
            Assert.Fail();
    }
}

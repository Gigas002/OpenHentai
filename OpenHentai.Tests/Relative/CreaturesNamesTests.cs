using System.Globalization;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class CreaturesNamesTests
{
    [Test]
    public void ConstructorTest()
    {
        var creatureMock = new Mock<Creature>();
        var textMock = new Mock<LanguageSpecificTextInfo>();

        var cn1 = new CreaturesNames();
        var cn2 = new CreaturesNames(creatureMock.Object, textMock.Object);
        var cn3 = new CreaturesNames(creatureMock.Object, "name", "default");
        var cn4 = new CreaturesNames(creatureMock.Object, "name", CultureInfo.InvariantCulture);
    }

    [Test]
    public void PropertiesTest()
    {
        var creatureMock = new Mock<Creature>();

        var cn = new CreaturesNames
        {
            Id = 1,
            Entity = creatureMock.Object,
            Text = "name",
            Language = "default"
        };
    }

    [Test]
    public void GetLanguageSpecificTextInfoTest()
    {
        const string str = "default::name";

        var creatureMock = new Mock<Creature>();

        var cn = new CreaturesNames(creatureMock.Object, new(str));

        var lsti = cn.GetLanguageSpecificTextInfo();

        if (!str.Equals(lsti.ToString(), StringComparison.Ordinal))
            Assert.Fail();
    }
}
using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class ColoredInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var ci1 = new ColoredInfo();
        var ci2 = new ColoredInfo(Color.Colored, true);
    }

    [Test]
    public void PropertiesTest()
    {
        var ci = new ColoredInfo
        {
            IsOfficial = false,
            Color = Color.BlackWhite
        };
    }
}

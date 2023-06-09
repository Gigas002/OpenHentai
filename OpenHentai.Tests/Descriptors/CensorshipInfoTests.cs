using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class CensorshipInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var ci1 = new CensorshipInfo();
        var ci2 = new CensorshipInfo(Censorship.None, true);
    }

    [Test]
    public void PropertiesTest()
    {
        var ci = new CensorshipInfo
        {
            IsOfficial = false,
            Censorship = Censorship.Mosaic
        };
    }
}

using System.Globalization;
using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class LanguageInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var li1 = new LanguageInfo();
        var li2 = new LanguageInfo("en-US", false);
        var li3 = new LanguageInfo(new CultureInfo("ja-JP"));
    }

    [Test]
    public void PropertiesTest()
    {
        var eli = new LanguageInfo
        {
            Language = new CultureInfo("en-US"),
            IsOfficial = true
        };
    }
}

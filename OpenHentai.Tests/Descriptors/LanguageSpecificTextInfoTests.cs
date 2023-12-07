using System.Globalization;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class LanguageSpecificTextInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var lsti1 = new LanguageSpecificTextInfo();
        var lsti2 = new LanguageSpecificTextInfo("default::text");
        var lsti3 = new LanguageSpecificTextInfo("text", "en-US");
        var lsti4 = new LanguageSpecificTextInfo("text", new CultureInfo("ja-JP"));
    }

    [Test]
    public void PropertiesTest()
    {
        var lsti = new LanguageSpecificTextInfo
        {
            Language = "default",
            Text = "text"
        };
    }

    [Test]
    public void ToStringTest()
    {
        var text = "default::text";

        var lsti = new LanguageSpecificTextInfo(text);
        var str = lsti.ToString();

        if (!text.Equals(str, StringComparison.Ordinal))
            Assert.Fail();
    }
}

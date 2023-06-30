using System.Globalization;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.JsonConverters;

public class JsonConverterTests
{
    private const string Json = "{\"language\":\"en-US\",\"is_official\":true}";

    [Test]
    public void SerializationTest()
    {
        var ci = new CultureInfo("en-US");
        var liMock = new Mock<LanguageInfo>(ci, true);

        var ser = JsonSerializer.Serialize(liMock.Object, Essential.JsonSerializerOptions);

        if (!ser.Equals(Json, StringComparison.Ordinal))
            Assert.Fail();
    }

    [Test]
    public void DeserializationTest()
    {
        var ci = new CultureInfo("en-US");
        var liMock = new Mock<LanguageInfo>(ci, true);

        var obj = JsonSerializer.Deserialize<LanguageInfo>(Json, Essential.JsonSerializerOptions);

        if (!obj.Language.ToString().Equals(liMock.Object.Language.ToString(), StringComparison.Ordinal) 
            || obj.IsOfficial != liMock.Object.IsOfficial)
            Assert.Fail();
    }
}

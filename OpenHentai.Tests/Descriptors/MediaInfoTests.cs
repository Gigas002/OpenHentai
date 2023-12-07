using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class MediaInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var mi1 = new MediaInfo();
        var mi2 = new MediaInfo(new Uri("https://localhost:5230"), MediaType.Image, true);
        var mi3 = new MediaInfo("https://localhost:5230", MediaType.Video);
    }

    [Test]
    public void PropertiesTest()
    {
        var mi = new MediaInfo
        {
            Source = new Uri("https://localhost:5230"),
            Type = MediaType.Unknown,
            IsMain = false
        };
    }
}

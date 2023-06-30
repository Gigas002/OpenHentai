using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.Descriptors;

public class ExternalLinkInfoTests
{
    [Test]
    public void ConstructorTest()
    {
        var eli1 = new ExternalLinkInfo();
        var eli2 = new ExternalLinkInfo("title", new Uri("https://localhost:5230/"));
        var eli3 = new ExternalLinkInfo("title", "https://localhost:5230/");
    }

    [Test]
    public void PropertiesTest()
    {
        var eli = new ExternalLinkInfo
        {
            Title = "Title",
            Link = new Uri("https://localhost:5230/"),
            OfficialStatus = Statuses.OfficialStatus.Official,
            PaidStatus = Statuses.PaidStatus.Free
        };

        var descMock = new Mock<LanguageSpecificTextInfo>("default::descr");
        eli.Description.Add(descMock.Object);
    }
}

using System.Globalization;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;

namespace OpenHentai.Tests.Relative;

public class AuthorsNamesTests
{
    [Test]
    public void ConstructorTest()
    {
        var authorMock = new Mock<Author>();
        var textMock = new Mock<LanguageSpecificTextInfo>();

        var an1 = new AuthorsNames();
        var an2 = new AuthorsNames(authorMock.Object, textMock.Object);
        var an3 = new AuthorsNames(authorMock.Object, "name", "default");
        var an4 = new AuthorsNames(authorMock.Object, "name", CultureInfo.InvariantCulture);
    }

    [Test]
    public void PropertiesTest()
    {
        var authorMock = new Mock<Author>();

        var an = new AuthorsNames
        {
            Id = 1,
            Entity = authorMock.Object,
            Text = "name",
            Language = "default"
        };
    }

    [Test]
    public void GetLanguageSpecificTextInfoTest()
    {
        const string str = "default::name";

        var authorMock = new Mock<Author>();

        var an = new AuthorsNames(authorMock.Object, new(str));

        var lsti = an.GetLanguageSpecificTextInfo();

        if (!str.Equals(lsti.ToString(), StringComparison.Ordinal))
            Assert.Fail();
    }
}
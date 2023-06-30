using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Relative;

public class AuthorsCreationsTests
{
    [Test]
    public void ConstructorTest()
    {
        var ac1 = new AuthorsCreations();

        var authorMock = new Mock<Author>();
        var creationMock = new Mock<Creation>();

        var ac2 = new AuthorsCreations(authorMock.Object, creationMock.Object, AuthorRole.MainArtist);
    }

    [Test]
    public void PropertiesTest()
    {
        var authorMock = new Mock<Author>();
        var creationMock = new Mock<Creation>();

        var ac = new AuthorsCreations
        {
            Origin = authorMock.Object,
            Related = creationMock.Object,
            Relation = AuthorRole.SecondaryArtist
        };
    }
}

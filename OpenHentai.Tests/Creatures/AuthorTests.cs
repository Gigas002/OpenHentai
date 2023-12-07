using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Creatures;

public class AuthorTests
{
    [Test]
    public void ConstructorTest()
    {
        var author1 = new Author();
        var author2 = new Author(2);

        var namedAuthor1 = new Author("default::name 1");

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name 2");
        var namedAuthor2 = new Author(nameMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var author = new Author();

        var nameMock = new Mock<AuthorsNames>();
        var circleMock = new Mock<Circle>();
        var linkMock = new Mock<ExternalLinkInfo>();
        var creationMock = new Mock<AuthorsCreations>();

        author.AuthorNames.Add(nameMock.Object);
        author.Circles.Add(circleMock.Object);
        author.ExternalLinks.Add(linkMock.Object);
        author.Creations.Add(creationMock.Object);
    }

    [Test]
    public void GetAuthorNamesTest()
    {
        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name");
        var author = new Author(nameMock.Object);

        var names = author.GetAuthorNames();

        var name = names.FirstOrDefault(t => t.Language == nameMock.Object.Language
                                            && t.Text == nameMock.Object.Text);

        if (name is null)
            Assert.Fail();
    }

    [Test]
    public void AddAuthorNamesTest()
    {
        var author = new Author();

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::title");

        author.AddAuthorNames(new List<LanguageSpecificTextInfo> { nameMock.Object });

        var title = author.GetAuthorNames().FirstOrDefault(t => t.Language == nameMock.Object.Language
                                            && t.Text == nameMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddAuthorNameTest()
    {
        var author = new Author();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title1");

        author.AddAuthorName(titleMock.Object);
        author.AddAuthorName("default::title2");

        var names = author.GetAuthorNames();

        if (names is null || !names.Any())
            Assert.Fail();
    }

    [Test]
    public void GetCreationsTest()
    {
        var creationMock = new Mock<Creation>();
        var author = new Author();
        var authorCreationsMock = new Mock<AuthorsCreations>(author, creationMock.Object, AuthorRole.MainArtist);
        author.Creations.Add(authorCreationsMock.Object);

        var creations = author.GetCreations();

        if (creations is null || creations.Count == 0)
            Assert.Fail();
    }

    [Test]
    public void AddCreationsTest()
    {
        var creationMock = new Mock<Creation>();
        var creationsMock = new Mock<Dictionary<Creation, AuthorRole>>();
        creationsMock.Object.Add(creationMock.Object, AuthorRole.SecondaryArtist);

        var author = new Author();

        author.AddCreations(creationsMock.Object);

        var creations = author.GetCreations();

        if (creations is null || creations.Count == 0)
            Assert.Fail();
    }

    [Test]
    public void AddCreationTest()
    {
        var manga1 = new Manga(1);
        var manga2 = new Manga(2);
        var authorCreation = new KeyValuePair<Creation, AuthorRole>(manga1, AuthorRole.MainPageIllustrator);

        var author = new Author();

        author.AddCreation(authorCreation);
        author.AddCreation(manga2, AuthorRole.Unknown);

        var creations = author.GetCreations();

        if (creations is null || creations.Count == 0)
            Assert.Fail();
    }
}

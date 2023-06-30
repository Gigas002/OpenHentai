using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Tags;

namespace OpenHentai.Tests;

public class CreationTests
{
    [Test]
    public void ConstructorTest()
    {
        var creation1 = new Creation();
        var creation2 = new Creation(2);

        var namedCreation1 = new Creation("default::title 1");

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title 2");
        var namedCircle2 = new Creation(titleMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var creation = new Creation
        {
            Id = 1,
            PublishStarted = DateTime.Now,
            PublishEnded = DateTime.Now,
            Rating = Rating.R18,
            Status = PublishStatus.Published
        };

        var titleMock = new Mock<CreationsTitles>();
        var authorMock = new Mock<AuthorsCreations>();
        var circleMock = new Mock<Circle>();
        var sourceMock = new Mock<ExternalLinkInfo>();
        var descMock = new Mock<LanguageSpecificTextInfo>();
        var relationMock = new Mock<CreationsRelations>();
        var characterMock = new Mock<CreationsCharacters>();
        var mediaMock = new Mock<MediaInfo>();
        var languageMock = new Mock<LanguageInfo>();
        var censorMock = new Mock<CensorshipInfo>();
        var tagMock = new Mock<Tag>();

        creation.Titles.Add(titleMock.Object);
        creation.Authors.Add(authorMock.Object);
        creation.Circles.Add(circleMock.Object);
        creation.Sources.Add(sourceMock.Object);
        creation.Description.Add(descMock.Object);
        creation.Relations.Add(relationMock.Object);
        creation.Characters.Add(characterMock.Object);
        creation.Media.Add(mediaMock.Object);
        creation.Languages.Add(languageMock.Object);
        creation.Censorship.Add(censorMock.Object);
        creation.Tags.Add(tagMock.Object);
    }

    [Test]
    public void GetTitlesTest()
    {
        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");
        var creation = new Creation(titleMock.Object);

        var titles = creation.GetTitles();

        var title = titles.FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitlesTest()
    {
        var creation = new Creation();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");

        creation.AddTitles(new List<LanguageSpecificTextInfo> { titleMock.Object });

        var title = creation.GetTitles().FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitleTest()
    {
        var creation = new Creation();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title1");

        creation.AddTitle(titleMock.Object);
        creation.AddTitle("default::title2");

        var titles = creation.GetTitles();

        if (titles is null || !titles.Any())
            Assert.Fail();
    }

    [Test]
    public void GetAuthorsTest()
    {
        var authorMock = new Mock<Author>();
        var creation = new Creation();
        var authorsCreationsMock = new Mock<AuthorsCreations>(authorMock.Object, creation, AuthorRole.MainArtist);
        creation.Authors.Add(authorsCreationsMock.Object);

        var authors = creation.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void AddAuthorsTest()
    {
        var authorMock = new Mock<Author>();
        var authorsMock = new Mock<Dictionary<Author, AuthorRole>>();
        authorsMock.Object.Add(authorMock.Object, AuthorRole.MainPageIllustrator);

        var creation = new Creation();

        creation.AddAuthors(authorsMock.Object);

        var authors = creation.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void AddAuthorTest()
    {
        var author1 = new Author(1);
        var author2 = new Author(2);
        var authorRole = new KeyValuePair<Author, AuthorRole>(author1, AuthorRole.SecondaryArtist);

        var creation = new Creation();

        creation.AddAuthor(authorRole);
        creation.AddAuthor(author2, AuthorRole.Unknown);

        var authors = creation.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void GetRelationsTest()
    {
        var creationMock = new Mock<Creation>();
        var creation = new Creation();
        var creationsRelationsMock = new Mock<CreationsRelations>(creationMock.Object, creation, CreationRelations.Parent);
        creation.Relations.Add(creationsRelationsMock.Object);

        var relations = creation.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddRelationsTest()
    {
        var creationMock = new Mock<Creation>();
        var relationsMock = new Mock<Dictionary<Creation, CreationRelations>>();
        relationsMock.Object.Add(creationMock.Object, CreationRelations.Master);

        var creation = new Creation();

        creation.AddRelations(relationsMock.Object);

        var relations = creation.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddRelationTest()
    {
        var creation1 = new Creation(1);
        var creation2 = new Creation(2);
        var creationRelation = new KeyValuePair<Creation, CreationRelations>(creation1, CreationRelations.Child);

        var creation = new Creation();

        creation.AddRelation(creationRelation);
        creation.AddRelation(creation2, CreationRelations.Slave);

        var relations = creation.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void GetCharactersTest()
    {
        var characterMock = new Mock<Character>();
        var creation = new Creation();
        var creationsCharactersMock = new Mock<CreationsCharacters>(creation, characterMock.Object, CharacterRole.Main);
        creation.Characters.Add(creationsCharactersMock.Object);

        var characters = creation.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCharactersTest()
    {
        var characterMock = new Mock<Character>();
        var characterRoleMock = new Mock<Dictionary<Character, CharacterRole>>();
        characterRoleMock.Object.Add(characterMock.Object, CharacterRole.Secondary);

        var creation = new Creation();

        creation.AddCharacters(characterRoleMock.Object);

        var characters = creation.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCharacterTest()
    {
        var character1 = new Character(1);
        var character2 = new Character(2);
        var characterRole = new KeyValuePair<Character, CharacterRole>(character1, CharacterRole.Cosplay);

        var creation = new Creation();

        creation.AddCharacter(characterRole);
        creation.AddCharacter(character2, CharacterRole.Unknown);

        var characters = creation.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }
}

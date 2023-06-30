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

// TODO: probably should be divided onto MangaTests and CreationTests

public class MangaTests
{
    [Test]
    public void ConstructorTest()
    {
        var manga1 = new Manga();
        var manga2 = new Manga(2);

        var namedManga1 = new Manga("default::title 1");

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title 2");
        var namedCircle2 = new Manga(titleMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var manga = new Manga
        {
            Id = 1,
            PublishStarted = DateTime.Now,
            PublishEnded = DateTime.Now,
            Rating = Rating.R18,
            Status = PublishStatus.Published,
            Length = 0,
            Volumes = 0,
            Chapters = 0,
            HasImages = true
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
        var ciMock = new Mock<ColoredInfo>();

        manga.Titles.Add(titleMock.Object);
        manga.Authors.Add(authorMock.Object);
        manga.Circles.Add(circleMock.Object);
        manga.Sources.Add(sourceMock.Object);
        manga.Description.Add(descMock.Object);
        manga.Relations.Add(relationMock.Object);
        manga.Characters.Add(characterMock.Object);
        manga.Media.Add(mediaMock.Object);
        manga.Languages.Add(languageMock.Object);
        manga.Censorship.Add(censorMock.Object);
        manga.Tags.Add(tagMock.Object);
        manga.ColoredInfo.Add(ciMock.Object);
    }

    [Test]
    public void GetTitlesTest()
    {
        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");
        var manga = new Manga(titleMock.Object);

        var titles = manga.GetTitles();

        var title = titles.FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitlesTest()
    {
        var manga = new Manga();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title");

        manga.AddTitles(new List<LanguageSpecificTextInfo> { titleMock.Object });

        var title = manga.GetTitles().FirstOrDefault(t => t.Language == titleMock.Object.Language
                                            && t.Text == titleMock.Object.Text);

        if (title is null)
            Assert.Fail();
    }

    [Test]
    public void AddTitleTest()
    {
        var manga = new Manga();

        var titleMock = new Mock<LanguageSpecificTextInfo>("default::title1");

        manga.AddTitle(titleMock.Object);
        manga.AddTitle("default::title2");

        var titles = manga.GetTitles();

        if (titles is null || !titles.Any())
            Assert.Fail();
    }

    [Test]
    public void GetAuthorsTest()
    {
        var authorMock = new Mock<Author>();
        var manga = new Manga();
        var authorsCreationsMock = new Mock<AuthorsCreations>(authorMock.Object, manga, AuthorRole.MainArtist);
        manga.Authors.Add(authorsCreationsMock.Object);

        var authors = manga.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void AddAuthorsTest()
    {
        var authorMock = new Mock<Author>();
        var authorsMock = new Mock<Dictionary<Author, AuthorRole>>();
        authorsMock.Object.Add(authorMock.Object, AuthorRole.MainPageIllustrator);

        var manga = new Manga();

        manga.AddAuthors(authorsMock.Object);

        var authors = manga.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void AddAuthorTest()
    {
        var author1 = new Author(1);
        var author2 = new Author(2);
        var authorRole = new KeyValuePair<Author, AuthorRole>(author1, AuthorRole.SecondaryArtist);

        var manga = new Manga();

        manga.AddAuthor(authorRole);
        manga.AddAuthor(author2, AuthorRole.Unknown);

        var authors = manga.GetAuthors();

        if (authors is null || !authors.Any())
            Assert.Fail();
    }

    [Test]
    public void GetRelationsTest()
    {
        var mangaMock = new Mock<Manga>();
        var manga = new Manga();
        var creationsRelationsMock = new Mock<CreationsRelations>(mangaMock.Object, manga, CreationRelations.Parent);
        manga.Relations.Add(creationsRelationsMock.Object);

        var relations = manga.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddRelationsTest()
    {
        var mangaMock = new Mock<Manga>();
        var relationsMock = new Mock<Dictionary<Creation, CreationRelations>>();
        relationsMock.Object.Add(mangaMock.Object, CreationRelations.Master);

        var manga = new Manga();

        manga.AddRelations(relationsMock.Object);

        var relations = manga.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddRelationTest()
    {
        var manga1 = new Manga(1);
        var manga2 = new Manga(2);
        var creationRelation = new KeyValuePair<Creation, CreationRelations>(manga1, CreationRelations.Child);

        var manga = new Manga();

        manga.AddRelation(creationRelation);
        manga.AddRelation(manga2, CreationRelations.Slave);

        var relations = manga.GetRelations();

        if (relations is null || !relations.Any())
            Assert.Fail();
    }

    [Test]
    public void GetCharactersTest()
    {
        var characterMock = new Mock<Character>();
        var manga = new Manga();
        var creationsCharactersMock = new Mock<CreationsCharacters>(manga, characterMock.Object, CharacterRole.Main);
        manga.Characters.Add(creationsCharactersMock.Object);

        var characters = manga.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCharactersTest()
    {
        var characterMock = new Mock<Character>();
        var characterRoleMock = new Mock<Dictionary<Character, CharacterRole>>();
        characterRoleMock.Object.Add(characterMock.Object, CharacterRole.Secondary);

        var manga = new Manga();

        manga.AddCharacters(characterRoleMock.Object);

        var characters = manga.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCharacterTest()
    {
        var character1 = new Character(1);
        var character2 = new Character(2);
        var characterRole = new KeyValuePair<Character, CharacterRole>(character1, CharacterRole.Cosplay);

        var manga = new Manga();

        manga.AddCharacter(characterRole);
        manga.AddCharacter(character2, CharacterRole.Unknown);

        var characters = manga.GetCharacters();

        if (characters is null || !characters.Any())
            Assert.Fail();
    }
}

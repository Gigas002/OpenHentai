using Moq;
using OpenHentai.Repositories;
using OpenHentai.WebAPI.Controllers;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Tests;

public sealed class MangaControllerTests
{
    private const int Id = 1;

    [SetUp]
    public void Setup() { }

    #region GET

    [Test]
    public void GetAllMangaTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetManga())
            .Returns(new List<Manga>() { new Manga(Id) });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = controller.GetManga();

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetMangaTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Manga>(Id))
            .ReturnsAsync(new Manga(Id));

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetMangaAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTitlesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetTitlesAsync(Id))
            .ReturnsAsync(new List<CreationsTitles>() { new CreationsTitles() });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetTitlesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetAuthorsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetAuthorsAsync(Id))
            .ReturnsAsync(new List<AuthorsCreations>() { new AuthorsCreations() });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetAuthorsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCirclesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetCirclesAsync(Id))
            .ReturnsAsync(new List<Circle>() { new Circle(Id) });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetCirclesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetRelationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetRelationsAsync(Id))
            .ReturnsAsync(new List<CreationsRelations>() { new CreationsRelations() });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetRelationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCharactersTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetCharactersAsync(Id))
            .ReturnsAsync(new List<CreationsCharacters>() { new CreationsCharacters() });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetCharactersAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTagsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetTagsAsync(Id))
            .ReturnsAsync(new List<Tag>() { new Tag() });

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.GetTagsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST

    [Test]
    public async Task PostMangaTest()
    {
        // Arrange
        var mangaMock = new Mock<Manga>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddEntryAsync(mangaMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PostMangaAsync(mangaMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostTitlesTest()
    {
        // Arrange
        var titlesMock = new Mock<HashSet<LanguageSpecificTextInfo>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddTitlesAsync(Id, titlesMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PostTitlesAsync(Id, titlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostRelationsTest()
    {
        // Arrange
        var relationsMock = new Mock<Dictionary<ulong, CreationRelations>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddRelationsAsync(Id, relationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PostRelationsAsync(Id, relationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PUT

    [Test]
    public async Task PutAuthorsTest()
    {
        // Arrange
        var authorsMock = new Mock<Dictionary<ulong, AuthorRole>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddAuthorsAsync(Id, authorsMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PutAuthorsAsync(Id, authorsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PutCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCharactersTest()
    {
        // Arrange
        var charactersMock = new Mock<Dictionary<ulong, CharacterRole>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddCharactersAsync(Id, charactersMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PutCharactersAsync(Id, charactersMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.AddTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PutTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    public async Task DeleteMangaTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveEntryAsync<Manga>(Id))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteMangaAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTitlesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveTitlesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTitlesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteAuthorsTest()
    {
        // Arrange
        var authorsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveAuthorsAsync(Id, authorsMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteAuthorsAsync(Id, authorsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteRelationsTest()
    {
        // Arrange
        var relatedMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveRelationsAsync(Id, relatedMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteRelationsAsync(Id, relatedMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCharactersTest()
    {
        // Arrange
        var charactersMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveCharactersAsync(Id, charactersMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCharactersAsync(Id, charactersMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.RemoveTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    public async Task PatchMangaTest()
    {
        // Arrange
        var manga = new Manga(Id);
        var operationsMock = new Mock<List<Operation<Manga>>>();
        var repositoryMock = new Mock<IMangaRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Manga>(Id))
            .ReturnsAsync(manga);
        repositoryMock.Setup(r => r.SaveChangesAsync());

        using var controller = new MangaController(repositoryMock.Object);

        // Act
        var response = await controller.PatchMangaAsync(Id, operationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

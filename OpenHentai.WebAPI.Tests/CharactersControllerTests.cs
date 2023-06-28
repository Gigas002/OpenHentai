using Moq;
using OpenHentai.Repositories;
using OpenHentai.Creatures;
using OpenHentai.WebAPI.Controllers;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using SystemTextJsonPatch.Operations;

namespace OpenHentai.WebAPI.Tests;

public sealed class CharactersControllerTests
{
    private const int Id = 1;

    [SetUp]
    public void Setup() { }

    #region GET

    [Test]
    public void GetCharactersTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetCharacters())
            .Returns(new List<Character>() { new Character(Id) });

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = controller.GetCharacters();

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCharacterTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Character>(Id))
            .ReturnsAsync(new Character(Id));

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.GetCharacterAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCreationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetCreationsAsync(Id))
            .ReturnsAsync(new List<CreationsCharacters>() { new CreationsCharacters() });

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.GetCreationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetNamesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetNamesAsync(Id))
            .ReturnsAsync(new List<CreaturesNames>() { new CreaturesNames() });

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.GetNamesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTagsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetTagsAsync(Id))
            .ReturnsAsync(new List<Tag>() { new Tag() });

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.GetTagsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetRelationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetRelationsAsync(Id))
            .ReturnsAsync(new List<CreaturesRelations>() { new CreaturesRelations() });

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.GetRelationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST

    [Test]
    public async Task PostCharacterTest()
    {
        // Arrange
        var characterMock = new Mock<Character>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.AddEntryAsync(characterMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PostCharacterAsync(characterMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostNamesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<LanguageSpecificTextInfo>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.AddNamesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PostNamesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostRelationsTest()
    {
        // Arrange
        var relationsMock = new Mock<Dictionary<ulong, CreatureRelations>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.AddRelationsAsync(Id, relationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PostRelationsAsync(Id, relationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PUT

    [Test]
    public async Task PutCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<Dictionary<ulong, CharacterRole>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.AddCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PutCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.AddTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PutTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    public async Task DeleteCharacterTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.RemoveEntryAsync<Character>(Id))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCharacterAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.RemoveCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteNamesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.RemoveNamesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteNamesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.RemoveTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteRelationsTest()
    {
        // Arrange
        var relatedMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.RemoveRelationsAsync(Id, relatedMock.Object))
            .ReturnsAsync(true);

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteRelationsAsync(Id, relatedMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    public async Task PatchCharacterTest()
    {
        // Arrange
        var character = new Character(Id);
        var operationsMock = new Mock<List<Operation<Character>>>();
        var repositoryMock = new Mock<ICharactersRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Character>(Id))
            .ReturnsAsync(character);
        repositoryMock.Setup(r => r.SaveChangesAsync());

        using var controller = new CharactersController(repositoryMock.Object);

        // Act
        var response = await controller.PatchCharacterAsync(Id, operationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

using OpenHentai.Repositories;
using OpenHentai.Creatures;
using OpenHentai.WebAPI.Controllers;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;

#pragma warning disable CA2007

namespace OpenHentai.WebAPI.Tests;

public sealed class AuthorsControllerTests
{
    private const int Id = 1;

    [SetUp]
    public void Setup() { }

    #region GET

    [Test]
    public void GetAuthorsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetAuthors())
            .Returns(new List<Author>() { new Author(Id) });

        using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = controller.GetAuthors();

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetAuthorTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Author>(Id))
            .ReturnsAsync(new Author(Id));

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetAuthorAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public void GetAuthorsNamesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetAuthorsNames())
            .Returns(new List<AuthorsNames>() { new AuthorsNames() });

        using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = controller.GetAuthorsNames();

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetAuthorNamesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetAuthorNamesAsync(Id))
            .ReturnsAsync(new List<AuthorsNames>() { new AuthorsNames() });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetAuthorNamesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCirclesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetCirclesAsync(Id))
            .ReturnsAsync(new List<Circle>() { new Circle(Id) });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetCirclesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCreationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetCreationsAsync(Id))
            .ReturnsAsync(new List<AuthorsCreations>() { new AuthorsCreations() });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetCreationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetNamesTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetNamesAsync(Id))
            .ReturnsAsync(new List<CreaturesNames>() { new CreaturesNames() });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetNamesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTagsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetTagsAsync(Id))
            .ReturnsAsync(new List<Tag>() { new Tag() });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetTagsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetRelationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetRelationsAsync(Id))
            .ReturnsAsync(new List<CreaturesRelations>() { new CreaturesRelations() });

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.GetRelationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST

    [Test]
    public async Task PostAuthorTest()
    {
        // Arrange
        var authorMock = new Mock<Author>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddEntryAsync(authorMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PostAuthorAsync(authorMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostAuthorNamesTest()
    {
        // Arrange
        var authorNamesMock = new Mock<HashSet<LanguageSpecificTextInfo>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddAuthorNamesAsync(Id, authorNamesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PostAuthorNamesAsync(Id, authorNamesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostNamesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<LanguageSpecificTextInfo>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddNamesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PostNamesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostRelationsTest()
    {
        // Arrange
        var relationsMock = new Mock<Dictionary<ulong, CreatureRelations>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddRelationsAsync(Id, relationsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PostRelationsAsync(Id, relationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PUT

    [Test]
    public async Task PutCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PutCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<Dictionary<ulong, AuthorRole>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PutCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.AddTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PutTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    public async Task DeleteAuthorTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveEntryAsync<Author>(Id))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteAuthorAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteAuthorNamesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveAuthorNamesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteAuthorNamesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteNamesTest()
    {
        // Arrange
        var namesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveNamesAsync(Id, namesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteNamesAsync(Id, namesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteRelationsTest()
    {
        // Arrange
        var relatedMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.RemoveRelationsAsync(Id, relatedMock.Object))
            .ReturnsAsync(true);

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteRelationsAsync(Id, relatedMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    public async Task PatchAuthorTest()
    {
        // Arrange
        var author = new Author(Id);
        var operationsMock = new Mock<List<Operation<Author>>>();
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Author>(Id))
            .ReturnsAsync(author);
        repositoryMock.Setup(r => r.SaveChangesAsync());

        await using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = await controller.PatchAuthorAsync(Id, operationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

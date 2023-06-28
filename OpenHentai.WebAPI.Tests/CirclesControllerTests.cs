using Moq;
using OpenHentai.Repositories;
using OpenHentai.Creatures;
using OpenHentai.WebAPI.Controllers;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Tests;

public sealed class CirclesControllerTests
{
    private const int Id = 1;

    [SetUp]
    public void Setup() { }

    #region GET

    [Test]
    public void GetCirclesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetCircles())
            .Returns(new List<Circle>() { new Circle(Id) });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = controller.GetCircles();

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCircleTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Circle>(Id))
            .ReturnsAsync(new Circle(Id));

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.GetCircleAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public void GetAllTitlesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetAllTitles())
            .Returns(new List<CirclesTitles>() { new CirclesTitles() });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = controller.GetAllTitles();

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTitlesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetTitlesAsync(Id))
            .ReturnsAsync(new List<CirclesTitles>() { new CirclesTitles() });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.GetTitlesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetAuthorsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetAuthorsAsync(Id))
            .ReturnsAsync(new List<Author>() { new Author(Id) });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.GetAuthorsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCreationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetCreationsAsync(Id))
            .ReturnsAsync(new List<Creation>() { new Creation(Id) });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.GetCreationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTagsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetTagsAsync(Id))
            .ReturnsAsync(new List<Tag>() { new Tag() });

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.GetTagsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST

    [Test]
    public async Task PostCircleTest()
    {
        // Arrange
        var circleMock = new Mock<Circle>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.AddEntryAsync(circleMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.PostCircleAsync(circleMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PostTitlesTest()
    {
        // Arrange
        var circleNamesMock = new Mock<HashSet<LanguageSpecificTextInfo>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.AddTitlesAsync(Id, circleNamesMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.PostTitlesAsync(Id, circleNamesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PUT

    [Test]
    public async Task PutAuthorsTest()
    {
        // Arrange
        var authorsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.AddAuthorsAsync(Id, authorsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.PutAuthorsAsync(Id, authorsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.AddCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

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
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.AddTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.PutTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    public async Task DeleteCircleTest()
    {
        // Arrange
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.RemoveEntryAsync<Circle>(Id))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCircleAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTitltesTest()
    {
        // Arrange
        var titlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.RemoveTitlesAsync(Id, titlesMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTitlesAsync(Id, titlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteAuthorsTest()
    {
        // Arrange
        var authorsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.RemoveAuthorsAsync(Id, authorsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteAuthorsAsync(Id, authorsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.RemoveCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteTagsTest()
    {
        // Arrange
        var tagsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.RemoveTagsAsync(Id, tagsMock.Object))
            .ReturnsAsync(true);

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTagsAsync(Id, tagsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    public async Task PatchCircleTest()
    {
        // Arrange
        var circle = new Circle(Id);
        var operationsMock = new Mock<List<Operation<Circle>>>();
        var repositoryMock = new Mock<ICirclesRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Circle>(Id))
            .ReturnsAsync(circle);
        repositoryMock.Setup(r => r.SaveChangesAsync());

        using var controller = new CirclesController(repositoryMock.Object);

        // Act
        var response = await controller.PatchCircleAsync(Id, operationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

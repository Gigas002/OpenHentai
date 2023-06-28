using OpenHentai.Repositories;
using OpenHentai.Creatures;
using OpenHentai.WebAPI.Controllers;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Creations;

#pragma warning disable CA2007

namespace OpenHentai.WebAPI.Tests;

public sealed class TagsControllerTests
{
    private const int Id = 1;

    [SetUp]
    public void Setup() { }

    #region GET

    [Test]
    public void GetTagsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetTags())
            .Returns(new List<Tag>() { new Tag(Id) });

        using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = controller.GetTags();

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetTagTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Tag>(Id))
            .ReturnsAsync(new Tag(Id));

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.GetTagAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCreaturesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetCreaturesAsync(Id))
            .ReturnsAsync(new List<Creature>() { new Creature(Id) });

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.GetCreationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCreationsTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetCreationsAsync(Id))
            .ReturnsAsync(new List<Creation>() { new Creation() });

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.GetCreationsAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task GetCirclesTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetCirclesAsync(Id))
            .ReturnsAsync(new List<Circle>() { new Circle(Id) });

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.GetCirclesAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST

    [Test]
    public async Task PostTagTest()
    {
        // Arrange
        var tagMock = new Mock<Tag>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.AddEntryAsync(tagMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.PostTagAsync(tagMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PUT

    [Test]
    public async Task PutCreaturesTest()
    {
        // Arrange
        var creaturesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.AddCreaturesAsync(Id, creaturesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.PutCreaturesAsync(Id, creaturesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.AddCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.PutCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task PutCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.AddCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.PutCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    public async Task DeleteTagTest()
    {
        // Arrange
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.RemoveEntryAsync<Tag>(Id))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteTagAsync(Id).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCreaturesTest()
    {
        // Arrange
        var creaturesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.RemoveCreaturesAsync(Id, creaturesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCreaturesAsync(Id, creaturesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

        [Test]
    public async Task DeleteCreationsTest()
    {
        // Arrange
        var creationsMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.RemoveCreationsAsync(Id, creationsMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCreationsAsync(Id, creationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    [Test]
    public async Task DeleteCirclesTest()
    {
        // Arrange
        var circlesMock = new Mock<HashSet<ulong>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.RemoveCirclesAsync(Id, circlesMock.Object))
            .ReturnsAsync(true);

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.DeleteCirclesAsync(Id, circlesMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    public async Task PatchTagTest()
    {
        // Arrange
        var tag = new Tag(Id);
        var operationsMock = new Mock<List<Operation<Tag>>>();
        var repositoryMock = new Mock<ITagsRepository>();
        repositoryMock.Setup(r => r.GetEntryAsync<Tag>(Id))
            .ReturnsAsync(tag);
        repositoryMock.Setup(r => r.SaveChangesAsync());

        await using var controller = new TagsController(repositoryMock.Object);

        // Act
        var response = await controller.PatchTagAsync(Id, operationsMock.Object).ConfigureAwait(false);

        // Assert
        if (!Helper.CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

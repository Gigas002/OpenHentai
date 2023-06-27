using Moq;
using OpenHentai.Repositories;
using OpenHentai.Creatures;
using OpenHentai.WebAPI.Controllers;

namespace OpenHentai.WebAPI.Tests;

public sealed class AuthorsControllerTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void GetAuthorsTest()
    {
        // Arrange
        var repositoryMock = new Mock<IAuthorsRepository>();
        repositoryMock
            .Setup(r => r.GetAuthors())
            .Returns(new List<Author>() { new Author(1) });

        using var controller = new AuthorsController(repositoryMock.Object);

        // Act
        var response = controller.GetAuthors();

        // Assert
        if (!Global.CheckResponse(response)) Assert.Fail();
    }
}

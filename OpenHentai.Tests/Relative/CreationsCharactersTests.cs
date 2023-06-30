using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Relative;

public class CreationsCharactersTests
{
    [Test]
    public void ConstructorTest()
    {
        var creationMock = new Mock<Creation>();
        var characterMock = new Mock<Character>();

        var an1 = new CreationsCharacters();
        var an2 = new CreationsCharacters(creationMock.Object, characterMock.Object, CharacterRole.Main);
    }

    [Test]
    public void PropertiesTest()
    {
        var creationMock = new Mock<Creation>();
        var characterMock = new Mock<Character>();

        var cc = new CreationsCharacters
        {
            Origin = creationMock.Object,
            Related = characterMock.Object,
            Relation = CharacterRole.Unknown
        };
    }
}
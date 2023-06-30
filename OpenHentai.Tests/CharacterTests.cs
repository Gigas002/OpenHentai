using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Tests;

public class CharacterTests
{
    [Test]
    public void ConstructorTest()
    {
        var character1 = new Character();
        var character2 = new Character(2);

        var namedCharacter1 = new Character("default::name 1");

        var nameMock = new Mock<LanguageSpecificTextInfo>("default::name 2");
        var namedCharacter2 = new Character(nameMock.Object);
    }

    [Test]
    public void PropertiesTest()
    {
        var character = new Character();

        var creationMock = new Mock<CreationsCharacters>();

        character.Creations.Add(creationMock.Object);
    }

    [Test]
    public void GetCreationsTest()
    {
        var creationMock = new Mock<Creation>();
        var character = new Character();
        var characterCreationsMock = new Mock<CreationsCharacters>(creationMock.Object, character, CharacterRole.Main);
        character.Creations.Add(characterCreationsMock.Object);

        var creations = character.GetCreations();

        if (creations is null || !creations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCreationsTest()
    {
        var creationMock = new Mock<Creation>();
        var creationsMock = new Mock<Dictionary<Creation, CharacterRole>>();
        creationsMock.Object.Add(creationMock.Object, CharacterRole.Secondary);

        var character = new Character();

        character.AddCreations(creationsMock.Object);

        var creations = character.GetCreations();

        if (creations is null || !creations.Any())
            Assert.Fail();
    }

    [Test]
    public void AddCreationTest()
    {
        var manga1 = new Manga(1);
        var manga2 = new Manga(2);
        var characterCreation = new KeyValuePair<Creation, CharacterRole>(manga1, CharacterRole.Cosplay);

        var character = new Character();

        character.AddCreation(characterCreation);
        character.AddCreation(manga2, CharacterRole.Unknown);

        var creations = character.GetCreations();

        if (creations is null || !creations.Any())
            Assert.Fail();
    }
}

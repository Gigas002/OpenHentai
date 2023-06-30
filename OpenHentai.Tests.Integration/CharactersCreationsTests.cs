using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Roles;

namespace OpenHentai.Tests.Integration;

public class CharactersCreationsTests : DatabaseTestsBase
{
    [Test]
    public void PushCharactersCreationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1M = new Mock<Character>("default::Unnamed male");
        var ymM2F = new Mock<Character>("default::Akaname");
        var aM1F = new Mock<Character>("default::Ajax");
        var aM2F = new Mock<Character>("default::Aliza");

        var ymM1 = new Mock<Manga>("default::Monokemono Shoya");
        var ymM2 = new Mock<Manga>("default::Monokemono");
        var aM1 = new Mock<Manga>("default::VictimGirls 24");
        var aM2 = new Mock<Manga>("default::VictimGirls 25");

        ymM1M.Object.AddCreation(ymM1.Object, CharacterRole.Main);
        ymM1M.Object.AddCreation(ymM2.Object, CharacterRole.Secondary);
        ymM2F.Object.AddCreation(ymM2.Object, CharacterRole.Main);
        aM1.Object.AddCharacter(aM1F.Object, CharacterRole.Main);
        aM2.Object.AddCharacter(aM2F.Object, CharacterRole.Main);

        db.SaveChanges();
    }
}
using OpenHentai.Roles;

namespace OpenHentai.Tests.Integration;

public class CharactersCreationsTests : DatabaseTestsBase
{
    // depends on PushCharactersTest(1)
    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersCreationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var characters = db.Characters.Include(a => a.Names).ToHashSet();

        var ymM1M = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Unnamed male"));
        var ymM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Akaname"));
        var aM1F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Ajax"));
        var aM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Aliza"));

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        ymM1M!.AddCreation(ymM1!, CharacterRole.Main);
        ymM1M.AddCreation(ymM2!, CharacterRole.Secondary);
        ymM2F!.AddCreation(ymM2!, CharacterRole.Main);
        aM1!.AddCharacter(aM1F!, CharacterRole.Main);
        aM2!.AddCharacter(aM2F!, CharacterRole.Main);

        db.SaveChanges();
    }
}
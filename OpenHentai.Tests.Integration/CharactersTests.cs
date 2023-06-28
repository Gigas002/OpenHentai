using OpenHentai.Creatures;
using OpenHentai.Relations;

namespace OpenHentai.Tests.Integration;

public class CharactersTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public void PushCharactersTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var ymM1M = new Character("default::Unnamed male")
        {
            Birthday = new DateTime(1900, 01, 01),
            Age = 25,
            Gender = Gender.Male
        };
        ymM1M.Description.Add(new("en-US::Protagonist of Monokemono Shoya"));

        var ymM2F = new Character("default::Akaname")
        {
            Birthday = new DateTime(1900, 01, 01),
            Age = 10,
            Gender = Gender.Female
        };
        ymM2F.Description.Add(new("en-US::Protagonist of Monokemono Yonya"));

        var aM1F = new Character("default::Ajax")
        {
            Birthday = new DateTime(1900, 01, 01),
            Age = 15,
            Gender = Gender.Female
        };
        aM1F.Description.Add(new("en-US::Azur lane character"));

        var aM2F = new Character("default::Aliza")
        {
            Birthday = new DateTime(1900, 01, 01),
            Age = 500,
            Gender = Gender.Female
        };
        aM2F.Description.Add(new("en-US::Granblue fantasy character"));

        db.Characters.AddRange(ymM1M, ymM2F, aM1F, aM2F);

        db.SaveChanges();
    }

    // depends on PushCharactersTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var characters = db.Characters.Include(a => a.Names).ToHashSet();

        var ymM1M = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Unnamed male"));
        var ymM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Akaname"));
        var aM1F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Ajax"));
        var aM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Aliza"));

        ymM1M!.AddRelation(ymM2F!, CreatureRelations.Unknown);
        aM1F!.AddRelation(aM2F!, CreatureRelations.Enemy);

        db.SaveChanges();
    }

    [Test]
    [Order(10)]
    public async Task ReadCharactersTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var characters = db.Characters.Include(c => c.Names)
                                      .Include(c => c.Creations)
                                      .ThenInclude(cc => cc.Origin)
                                      .Include(c => c.Tags)
                                      .Include(c => c.Relations)
                                      .ThenInclude(cr => cr.Related)
                                      .ToList();

        var json = await SerializeEntityAsync(characters).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Character>>(json).ConfigureAwait(false);
    }
}
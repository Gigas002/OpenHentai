namespace OpenHentai.Tests.Integration;

public class CharactersTagsTests : DatabaseTestsBase
{
        // depends on PushTagsTest(1)
    // depends on PushCharactersTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersTagsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var characters = db.Characters.Include(a => a.Names).ToHashSet();

        var ymM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Akaname"));
        var aM1F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Ajax"));
        var aM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Aliza"));

        var tags = db.Tags.Include(tag => tag.Creatures).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        ymM2F!.Tags.Add(loliTag!);
        loliTag!.Creatures.Add(aM1F!);
        alTag!.Creatures.Add(aM1F!);
        gfTag!.Creatures.Add(aM2F!);

        db.SaveChanges();
    }
}

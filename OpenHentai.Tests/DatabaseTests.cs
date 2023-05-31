using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Relative;
using OpenHentai.Database.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Roles;
using OpenHentai.Tags;
using System.Text.Json;

namespace OpenHentai.Tests;

public class DatabaseTests
{
    [SetUp]
    public void Setup()
    {
        // don't use this in prod, use migrations instead
        using (var db = new DatabaseContext())
        {
            // db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }

    #region Push tests

    [Test]
    [Order(1)]
    public void PushTagsTest()
    {
        using (var db = new DatabaseContext())
        {
            db.Database.EnsureCreated();
            var desc1 = new DescriptionInfo("en-US::Anime about camping");
            var desc2 = new DescriptionInfo("en-US::Second season of Yuru Camp");

            var tag1 = new Tag() { Category = TagCategory.Parody, Value = "Yuru Camp", Description = desc1 };
            var tag2 = new Tag() { Category = TagCategory.Parody, Value = "Yuru Camp Season 2", Description = desc2 };
            var tag3 = new Tag() { Category = TagCategory.Parody, Value = "JJBA" };

            tag2.Master = tag1; // SetMaster(tag1);

            db.Tags.AddRange(tag1, tag2, tag3);
            db.SaveChanges();
        }
    }

    [Test]
    [Order(2)]
    public void PushCreaturesTest()
    {
        using (var db = new DatabaseContext())
        {
            var author = new Author();
            author.Age = 10;

            var circle = new Circle();
            circle.Authors = new List<Author>() { author };

            var character = new Character();
            character.Age = 11;

            db.Authors.Add(author);
            db.Circles.Add(circle);
            db.Characters.Add(character);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(3)]
    public void PushCreation()
    {
        using (var db = new DatabaseContext())
        {
            var manga = new Manga() { Length = 10 };

            db.Mangas.Add(manga);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(4)]
    public void PushCharacterCreation()
    {
        using (var db = new DatabaseContext())
        {
            var mangas = db.Mangas.ToList();
            var chars = db.Characters.ToList();

            if (mangas.Count <= 0) Console.WriteLine("manganull");
            if (chars.Count <= 0) Console.WriteLine("charnull");

            foreach (var chara in chars)
            {
                var cc = new CreationsCharacters();
                cc.Creation = mangas.FirstOrDefault();
                cc.Character = chara;
                cc.CharacterRole = CharacterRole.Main;

                chara.InCreations.Add(cc);
            }

            db.SaveChanges();
        }
    }

    [Test]
    [Order(5)]
    public void PushCreaturesNames()
    {
        using (var db = new DatabaseContext())
        {
            var creatures = db.Creatures.ToList();

            foreach (var creature in creatures)
            {
                var name = new LanguageSpecificTextInfo($"en-US::Name {creature.Id}");
                var altName = new LanguageSpecificTextInfo($"en-US::Name_alt {creature.Id + 1000}");
                
                creature.Names.Add(new(name));
                creature.Names.Add(new(altName));
            }

            db.SaveChanges();
        }
    }

    #endregion

    #region Read tests

    [Test]
    [Order(1000)]
    public void ReadTagsTest()
    {
        using (var db = new DatabaseContext())
        {
            var tags = db.Tags.ToList();

            Console.WriteLine("Tags:");
            foreach (var tag in tags)
            {
                Console.WriteLine($"Tag: {tag.Value}");

                if (tag.Master is not null) Console.WriteLine($"Master: {tag.GetMaster()?.Value}");

                if (tag.Slaves?.Count() > 0)
                {
                    Console.WriteLine("Slaves:");
                    foreach (var slave in tag.Slaves)
                    {
                        Console.WriteLine($"- {slave.Value}");
                    }
                }
            }

            SerializeTags(tags);

            // var fantasyTags = Tag.GetTagWithSlaves(tags, "fantasy");
            // foreach (var tag in fantasyTags)
            //     Console.WriteLine($"Fantasy tag: {tag.Value}");
        }
    }

    #endregion

    public static void SerializeTags(IEnumerable<Tag> tags)
    {
        var options = new JsonSerializerOptions();
        // Requires net8+
        // options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;

        var json = JsonSerializer.Serialize(tags, options);

        File.WriteAllText("../list.json", json);
    }
}

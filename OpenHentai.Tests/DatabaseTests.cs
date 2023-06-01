using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Tags;
using System.Text.Json;

#pragma warning disable CA1303

namespace OpenHentai.Tests;

public class DatabaseTests
{
    [SetUp]
    public void Setup()
    {
        using var db = new DatabaseContext();
        
        // don't use this in prod, use migrations instead
        // db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    #region Push tests

    [Test]
    [Order(1)]
    public void PushTagsTest()
    {
        using var db = new DatabaseContext();
        
        var tag1 = new Tag { Category = TagCategory.Parody, Value = "Yuru Camp" };
        tag1.Description.Add(new("en-US::Anime about camping"));
        var tag2 = new Tag { Category = TagCategory.Parody, Value = "Yuru Camp Season 2" };
        tag2.Description.Add(new("en-US::Second season of Yuru Camp"));
        var tag3 = new Tag { Category = TagCategory.Parody, Value = "JJBA" };

        var creatureTag = new Tag { Category = TagCategory.BodyType, Value = "Adult" };

        tag2.Master = tag1;

        db.Tags.AddRange(tag1, tag2, tag3, creatureTag);
        db.SaveChanges();
    }

    [Test]
    [Order(2)]
    public void PushCreaturesTest()
    {
        using var db = new DatabaseContext();
        
        var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.BodyType);

        var author = new Author
        {
            Age = 10
        };
        author.Description.Add(new("en-US::Author descr 1"));
        author.Media.Add(new("https://google.com", MediaType.Image));
        author.Tags.Add(tag);
        author.ExternalLinks.Add(new("google", "https://google.com")
        {
            OfficialStatus = OfficialStatus.Official, PaidStatus = PaidStatus.Free,
        });

        var circle = new Circle();
        circle.Authors.Add(author);

        var character = new Character
        {
            Age = 11
        };
        character.Description.Add(new("en-US::Chara descr 1"));

        db.Authors.Add(author);
        db.Circles.Add(circle);
        db.Characters.Add(character);

        db.SaveChanges();
    }

    [Test]
    [Order(3)]
    public void PushCreationTest()
    {
        using var db = new DatabaseContext();
        
        var manga = new Manga
        {
            Length = 10,
        };
        manga.Media.Add(new("https://google.com", MediaType.Image));
        manga.Languages.Add(new("en-US", false));
        manga.Censorship.Add(new(Censorship.None, true));
        manga.Sources.Add(new("google", "https://google.com"));
        manga.Description.Add(new("en-US::Anime about camping"));
        manga.ColoredInfo.Add(new(Color.BlackWhite, true));
        
        db.Mangas.AddRange(manga);

        db.SaveChanges();
    }

    [Test]
    [Order(4)]
    public void PushCharacterCreationTest()
    {
        using var db = new DatabaseContext();
        
        var mangas = db.Mangas.ToList();
        var chars = db.Characters.ToList();

        foreach (var chara in chars)
        {
            chara.AddCreation(mangas.FirstOrDefault(), CharacterRole.Main);
        }

        db.SaveChanges();
    }

    [Test]
    [Order(5)]
    public void PushCreaturesNamesTest()
    {
        using var db = new DatabaseContext();
        
        var creatures = db.Creatures.ToList();

        foreach (var creature in creatures)
        {
            creature.AddNames(new List<LanguageSpecificTextInfo> 
            {
                new($"en-US::Name {creature.Id}"),
                new($"en-US::Name_alt {creature.Id + 1000}")
            });
        }

        db.SaveChanges();
    }

    [Test]
    [Order(6)]
    public void PushCreaturesRelationsTest()
    {
        using var db = new DatabaseContext();
        
        var creatures = db.Creatures.ToList();

        var author = creatures.FirstOrDefault(c => c is Author);
        var chara = creatures.FirstOrDefault(c => c is Character);
        chara.AddRelation(author, CreatureRelations.Enemy);

        db.SaveChanges();
    }

    [Test]
    [Order(7)]
    public void PushAuthorNamesTest()
    {
        using var db = new DatabaseContext();

        var author = db.Authors.FirstOrDefault();
        author.AddAuthorName(new("en-US", "Author Name"));

        db.SaveChanges();
    }

    [Test]
    [Order(8)]
    public void PushAuthorsCreationsTest()
    {
        using var db = new DatabaseContext();
       
        var authors = db.Authors.ToList();
        var creations = db.Creations.ToList();

        foreach (var author in authors)
        {
            author.AddCreation(creations.FirstOrDefault(), AuthorRole.MainArtist);
        }

        db.SaveChanges();
    }

    [Test]
    [Order(9)]
    public void PushCirclesTitlesTest()
    {
        using var db = new DatabaseContext();

        var circle = db.Circles.FirstOrDefault();
        circle.AddTitle(new("en-US", "Circle Title"));

        db.SaveChanges();
    }

    [Test]
    [Order(10)]
    public void PushCreationsTitlesTest()
    {
        using var db = new DatabaseContext();
        
        var creation = db.Creations.FirstOrDefault();
        
        creation.AddTitle(new("en-US", "Creation Title"));

        db.SaveChanges();
    }

    [Test]
    [Order(11)]
    public void PushCreationsRelationsTest()
    {
        using var db = new DatabaseContext();
        
        var creations = db.Creations.ToList();
            
        var creation = creations.FirstOrDefault();
        var relatedCreation = creations.LastOrDefault();
        
        creation.AddRelation(relatedCreation, CreationRelations.Parent);

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(1000)]
    public void ReadTagsTest()
    {
        using var db = new DatabaseContext();
        
        // TODO: find a way to use GetNames method instead of property
        var tags = db.Tags.Include(t => t.Creatures)
                     .ThenInclude(c => c.CreaturesNames)
                     .ToList();

        Console.WriteLine("Tags:");
        foreach (var tag in tags)
        {
            Console.WriteLine($"Tag: {tag.Value}");

            if (tag.Master is not null) Console.WriteLine($"- master: {tag.Master.Value}");

            if (tag.Slaves?.Count() > 0)
            {
                Console.WriteLine("- slaves:");
                foreach (var slave in tag.Slaves)
                {
                    Console.WriteLine($"  - {slave.Value}");
                }
            }

            if (tag.Creatures?.Count() <= 0) continue;
            
            Console.WriteLine("- creatures:");
            foreach (var creature in tag.Creatures)
            {
                Console.WriteLine($"  - {creature?.GetNames()?.FirstOrDefault()?.Text}");
            }
        }

        SerializeTags(tags);

        // var fantasyTags = Tag.GetTagWithSlaves(tags, "fantasy");
        // foreach (var tag in fantasyTags)
        //     Console.WriteLine($"Fantasy tag: {tag.Value}");
    }

    #endregion

    private static void SerializeTags(IEnumerable<Tag> tags)
    {
        var options = new JsonSerializerOptions();
        // Requires net8+
        // options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;

        var json = JsonSerializer.Serialize(tags, options);

        File.WriteAllText("../tags.json", json);
    }
}

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
        
        var desc1 = new List<LanguageSpecificTextInfo> { new("en-US::Anime about camping") };
        var desc2 = new List<LanguageSpecificTextInfo> { new("en-US::Second season of Yuru Camp") };

        var tag1 = new Tag { Category = TagCategory.Parody, Value = "Yuru Camp", Description = desc1 };
        var tag2 = new Tag { Category = TagCategory.Parody, Value = "Yuru Camp Season 2", Description = desc2 };
        var tag3 = new Tag { Category = TagCategory.Parody, Value = "JJBA" };

        var creatureTag = new Tag { Category = TagCategory.BodyType, Value = "Adult" };

        tag2.SetMaster(tag1);

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
            Age = 10,
            Description = new List<LanguageSpecificTextInfo> { new("en-US::Author descr 1") },
            Media = new List<MediaInfo> { new("https://google.com", MediaType.Image) },
            ExternalLinks = new List<ExternalLinkInfo>
            {
                new("google", "https://google.com")
                {
                    OfficialStatus = OfficialStatus.Official, PaidStatus = PaidStatus.Free,
                }
            },
            Tags = new List<Tag> { tag }
        };

        var circle = new Circle
        {
            Authors = new List<Author> { author }
        };

        var character = new Character
        {
            Age = 11,
            Description = new List<LanguageSpecificTextInfo> { new("en-US::Chara descr 1") }
        };

        db.Authors.Add(author);
        db.Circles.Add(circle);
        db.Characters.Add(character);

        db.SaveChanges();
    }

    [Test]
    [Order(3)]
    public void PushCreation()
    {
        using var db = new DatabaseContext();
        
        var manga = new Manga
        {
            Length = 10,
            Sources = new List<ExternalLinkInfo> { new("google", "https://google.com") },
            Description = new List<LanguageSpecificTextInfo> { new("en-US::Anime about camping") },
            Media = new List<MediaInfo> { new("https://google.com", MediaType.Image) },
            Languages = new List<LanguageInfo> { new("en-US", false) },
            Censorship = new List<CensorshipInfo> { new() { Censorship = Creations.Censorship.None, IsOfficial = true} },
            ColoredInfo = new List<ColoredInfo> { new() { Color = Color.BlackWhite, IsOfficial = true} }
        };
        // SerializeLangs(new List<LanguageInfo>() { new("en-US", false) });

        db.Mangas.AddRange(manga);

        db.SaveChanges();
    }

    [Test]
    [Order(4)]
    public void PushCharacterCreation()
    {
        using var db = new DatabaseContext();
        
        var mangas = db.Mangas.ToList();
        var chars = db.Characters.ToList();

        foreach (var chara in chars)
        {
            chara.SetCreations(new() 
            {
                { mangas.FirstOrDefault(), CharacterRole.Main}
            });
        }

        db.SaveChanges();
    }

    [Test]
    [Order(5)]
    public void PushCreaturesNames()
    {
        using var db = new DatabaseContext();
        
        var creatures = db.Creatures.ToList();

        foreach (var creature in creatures)
        {
            creature.SetNames(new List<LanguageSpecificTextInfo> 
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

        chara.SetRelations(new()
        {
            { author, CreatureRelations.Enemy }
        });

        db.SaveChanges();
    }

    [Test]
    [Order(7)]
    public void PushAuthorNames()
    {
        using var db = new DatabaseContext();

        var author = db.Authors.FirstOrDefault();
            
        author.SetAuthorNames(new List<LanguageSpecificTextInfo>
        {
            new("en-US", "Author Name")
        });

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
            author.SetCreations(new()
            {
                { creations.FirstOrDefault(), AuthorRole.MainArtist }
            });
        }

        db.SaveChanges();
    }

    [Test]
    [Order(9)]
    public void PushCirclesTitles()
    {
        using var db = new DatabaseContext();

        var circle = db.Circles.FirstOrDefault();
        circle.SetTitles(new List<LanguageSpecificTextInfo>
        {
            new("en-US", "Circle Title")
        });

        db.SaveChanges();
    }

    [Test]
    [Order(10)]
    public void PushCreationsTitles()
    {
        using var db = new DatabaseContext();
        
        var creation = db.Creations.FirstOrDefault();
            
        creation.SetTitles(new List<LanguageSpecificTextInfo>
        {
            new("en-US", "Creation Title")
        });

        db.SaveChanges();
    }

    [Test]
    [Order(11)]
    public void PushCreationsRelations()
    {
        using var db = new DatabaseContext();
        
        var creations = db.Creations.ToList();
            
        var creation = creations.FirstOrDefault();
        var relatedCreation = creations.LastOrDefault();
            
        creation.SetRelations(new()
        {
            { relatedCreation, CreationRelations.Parent }
        });

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(1000)]
    public void ReadTagsTest()
    {
        using var db = new DatabaseContext();
        
        // TODO: find a way to ise GetNames method instead of property
        var tags = db.Tags.Include(t => t.Creatures)
                     .ThenInclude(c => c.CreaturesNames)
                     .ToList();

        Console.WriteLine("Tags:");
        foreach (var tag in tags)
        {
            Console.WriteLine($"Tag: {tag.Value}");

            if (tag.GetMaster() is not null) Console.WriteLine($"- master: {tag.GetMaster()?.Value}");

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

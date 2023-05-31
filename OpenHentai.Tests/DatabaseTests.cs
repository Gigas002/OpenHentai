using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Relative;
using OpenHentai.Database.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
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
            var desc1 = new List<LanguageSpecificTextInfo>() { new("en-US::Anime about camping") };
            var desc2 = new List<LanguageSpecificTextInfo>() { new("en-US::Second season of Yuru Camp") };

            var tag1 = new Tag() { Category = TagCategory.Parody, Value = "Yuru Camp", Description = desc1 };
            var tag2 = new Tag() { Category = TagCategory.Parody, Value = "Yuru Camp Season 2", Description = desc2 };
            var tag3 = new Tag() { Category = TagCategory.Parody, Value = "JJBA" };

            var creatureTag = new Tag() { Category = TagCategory.BodyType, Value = "Adult" };

            tag2.Master = tag1; // SetMaster(tag1);

            db.Tags.AddRange(tag1, tag2, tag3, creatureTag);
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
            author.Description = new List<LanguageSpecificTextInfo>() { new("en-US::Author descr 1") };
            author.Media = new List<MediaInfo>() { new("https://google.com", MediaType.Image) };
            author.ExternalLinks = new List<ExternalLinkInfo>() { new("google", "https://google.com") { OfficialStatus = OfficialStatus.Official, PaidStatus = PaidStatus.Free,} };

            var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.BodyType);
            author.Tags = new List<Tag>() { tag };

            var circle = new Circle();
            circle.Authors = new List<Author>() { author };

            var character = new Character();
            character.Age = 11;
            character.Description = new List<LanguageSpecificTextInfo>() { new("en-US::Chara descr 1") };

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
            manga.Sources = new List<ExternalLinkInfo>() { new("google", "https://google.com") };
            manga.Description = new List<LanguageSpecificTextInfo>() { new("en-US::Anime about camping") };
            manga.Media = new List<MediaInfo>() { new("https://google.com", MediaType.Image) };
            manga.Languages = new List<LanguageInfo>() { new("en-US", false) };
            manga.Censorship = new List<CensorshipInfo>() { new() { Censorship = Creations.Censorship.None, IsOfficial = true} };
            manga.ColoredInfo = new List<ColoredInfo>() { new() { Color = Color.BlackWhite, IsOfficial = true} };
            // SerializeLangs(new List<LanguageInfo>() { new("en-US", false) });

            // var manga2 = new Manga() { Length = 100 };
            // manga.Sources.Add(new("https://bing.com"));
            // manga.Description = new DescriptionInfo("en-US::Anime about something else");

            db.Mangas.AddRange(manga); //, manga2);

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

                chara.InCreations = new List<CreationsCharacters>() { cc };
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

                creature.Names = new List<CreaturesNames>() { new(name), new(altName) };
            }

            db.SaveChanges();
        }
    }

    [Test]
    [Order(6)]
    public void PushCreaturesRelationsTest()
    {
        using (var db = new DatabaseContext())
        {
            var creatures = db.Creatures.ToList();

            var author = creatures.Where(c => c is Author).FirstOrDefault();
            var chara = creatures.Where(c => c is Character).FirstOrDefault();

            var cr = new CreaturesRelations();
            cr.Creature = chara;
            cr.RelatedCreature = author;
            cr.Relation = CreatureRelations.Enemy;

            db.CreaturesRelations.Add(cr);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(7)]
    public void PushAuthorNames()
    {
        using (var db = new DatabaseContext())
        {
            var authors = db.Authors.ToList();

            var authorName = new AuthorsNames(authors.FirstOrDefault(), "Author Name", "en-US");

            db.AuthorsNames.Add(authorName);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(8)]
    public void PushAuthorsCreationsTest()
    {
        using (var db = new DatabaseContext())
        {
            var authors = db.Authors.ToList();
            var creations = db.Creations.ToList();

            foreach (var author in authors)
            {
                var ac = new AuthorsCreations();
                ac.Author = author;
                ac.Creation = creations.FirstOrDefault();
                ac.Role = AuthorRole.MainArtist;

                author.Creations = new List<AuthorsCreations>() { ac };
            }

            db.SaveChanges();
        }
    }

    [Test]
    [Order(9)]
    public void PushCirclesTitles()
    {
        using (var db = new DatabaseContext())
        {
            var circles = db.Circles.ToList();

            var circleTitle = new CirclesTitles(circles.FirstOrDefault(), "Circle Title", "en-US");

            db.CirclesTitles.Add(circleTitle);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(10)]
    public void PushCreationsTitles()
    {
        using (var db = new DatabaseContext())
        {
            var creations = db.Creations.ToList();

            var creationTitle = new CreationsTitles(creations.FirstOrDefault(), "Creation Title", "en-US");

            db.CreationsTitles.Add(creationTitle);

            db.SaveChanges();
        }
    }

    [Test]
    [Order(11)]
    public void PushCreationsRelations()
    {
        using (var db = new DatabaseContext())
        {
            var creations = db.Creations.ToList();

            var cr = new CreationsRelations();
            cr.Creation = creations.FirstOrDefault();
            cr.RelatedCreation = creations.LastOrDefault();
            cr.Relation = CreationRelations.Parent;

            db.CreationsRelations.Add(cr);

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
            var tags = db.Tags.Include(t => t.Creatures)
                              .ThenInclude(c => c.Names)
                              .ToList();

            Console.WriteLine("Tags:");
            foreach (var tag in tags)
            {
                Console.WriteLine($"Tag: {tag.Value}");

                if (tag.Master is not null) Console.WriteLine($"- master: {tag.GetMaster()?.Value}");

                if (tag.Slaves?.Count() > 0)
                {
                    Console.WriteLine("- slaves:");
                    foreach (var slave in tag.Slaves)
                    {
                        Console.WriteLine($"  - {slave.Value}");
                    }
                }

                if (tag.Creatures?.Count() > 0)
                {
                    Console.WriteLine("- creatures:");
                    foreach(var creature in tag.Creatures)
                    {
                        Console.WriteLine($"  - {creature?.Names?.FirstOrDefault().Text}");
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

        File.WriteAllText("../tags.json", json);
    }

    public static void SerializeLangs(IEnumerable<LanguageInfo> langs)
    {
        var json = JsonSerializer.Serialize(langs);

        File.WriteAllText("../langs.json", json);
    }
}

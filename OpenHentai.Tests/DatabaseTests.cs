using OpenHentai.Creations;
using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;

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
        var charaTag = new Tag { Category = TagCategory.BodyType, Value = "Loli" };

        var circleTag = new Tag { Category = TagCategory.Personality, Value = "Yandere" };

        tag2.Master = tag1;

        db.Tags.AddRange(tag1, tag2, tag3, creatureTag, charaTag, circleTag);
        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsTest()
    {
        using var db = new DatabaseContext();

        var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.BodyType);

        var author = new Author
        {
            Age = 10
        };
        author.Description.Add(new("en-US::Author descr 1"));
        author.Media.Add(new("https://google.com", MediaType.Image));
        author.Tags.Add(tag!);
        author.ExternalLinks.Add(new("google", "https://google.com")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free,
        });

        db.Authors.Add(author);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersTest()
    {
        using var db = new DatabaseContext();

        var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.BodyType && t.Value == "Loli");

        var character = new Character
        {
            Age = 11
        };
        character.Description.Add(new("en-US::Chara descr 1"));
        character.Tags.Add(tag!);
        character.Media.Add(new("https://bing.com", MediaType.Image));

        db.Characters.Add(character);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    // depends on PushAuthorsTest(2)
    [Test]
    [Order(3)]
    public void PushCirclesTest()
    {
        using var db = new DatabaseContext();

        var author = db.Authors.FirstOrDefault();
        var tag = db.Tags.FirstOrDefault(t => t.Value == "Yandere");

        var circle = new Circle();
        circle.Authors.Add(author!);
        circle.Tags.Add(tag!);

        db.Circles.Add(circle);

        db.SaveChanges();
    }

    // depends on PushAuthorsTest(2)
    // or
    // depends on PushCharactersTest(2)
    [Test]
    [Order(3)]
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

    // depends on PushAuthorsTest(2)
    // depends on PushCharacterTest(2)
    [Test]
    [Order(3)]
    public void PushCreaturesRelationsTest()
    {
        using var db = new DatabaseContext();

        var creatures = db.Creatures.ToList();

        var author = creatures.FirstOrDefault(c => c is Author);
        var chara = creatures.FirstOrDefault(c => c is Character);
        chara?.AddRelation(author!, CreatureRelations.Enemy);

        db.SaveChanges();
    }

    // depends on PushAuthorsTest(2)
    [Test]
    [Order(3)]
    public void PushAuthorNamesTest()
    {
        using var db = new DatabaseContext();

        var author = db.Authors.FirstOrDefault();
        author!.AddAuthorName(new("Author Name", "en-US"));

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    // depends on PushCirclesTest(3)
    [Test]
    [Order(4)]
    public void PushMangaTest()
    {
        using var db = new DatabaseContext();

        var tags = db.Tags.Where(t => t.Category == TagCategory.Parody);
        var circle = db.Circles.FirstOrDefault();

        var manga1 = new Manga
        {
            Length = 10,
        };
        manga1.AddTitle(new("manga1", "en-US"));
        manga1.Media.Add(new("https://google.com", MediaType.Image));
        manga1.Languages.Add(new("en-US", false));
        manga1.Censorship.Add(new(Censorship.None, true));
        manga1.Sources.Add(new("google", "https://google.com"));
        manga1.Description.Add(new("en-US::Anime about camping"));
        manga1.ColoredInfo.Add(new(Color.BlackWhite, true));
        manga1.Tags.UnionWith(tags);
        manga1.Circles.Add(circle!);

        var manga2 = new Manga();
        manga2.AddTitle(new("manga2", "en-US"));

        var mangaCol = new Manga();
        mangaCol.AddTitle(new("mangaCol", "en-US"));

        db.Manga.AddRange(manga1, manga2, mangaCol);

        db.SaveChanges();
    }

    // depends on PushCirclesTest(3)
    [Test]
    [Order(4)]
    public void PushCirclesTitlesTest()
    {
        using var db = new DatabaseContext();

        var circle = db.Circles.FirstOrDefault();
        circle!.AddTitle(new("Circle Title", "en-US"));

        db.SaveChanges();
    }

    // depends on PushCharactersTest(2)
    // depends on PushMangaTest(4)
    [Test]
    [Order(5)]
    public void PushCharacterCreationTest()
    {
        using var db = new DatabaseContext();

        var mangas = db.Manga.ToList();
        var chars = db.Characters.ToList();

        foreach (var chara in chars)
        {
            chara.AddCreation(mangas.FirstOrDefault()!, CharacterRole.Main);
        }

        db.SaveChanges();
    }

    // depends on PushAuthorsTest(2)
    // depends on PushMangaTest(4)
    [Test]
    [Order(5)]
    public void PushAuthorsCreationsTest()
    {
        using var db = new DatabaseContext();

        var authors = db.Authors.ToList();
        var creations = db.Creations.ToList();

        foreach (var author in authors)
        {
            author.AddCreation(creations.FirstOrDefault()!, AuthorRole.MainArtist);
        }

        db.SaveChanges();
    }

    // depends on PushMangaTest(4)
    [Test]
    [Order(5)]
    public void PushCreationsTitlesTest()
    {
        using var db = new DatabaseContext();

        var creation = db.Creations.FirstOrDefault();

        creation!.AddTitle(new("Creation Title", "en-US"));

        db.SaveChanges();
    }

    // depends on PushMangaTest(4)
    [Test]
    [Order(5)]
    public void PushCreationsRelationsTest()
    {
        using var db = new DatabaseContext();

        var manga = db.Manga.ToHashSet();

        var manga1 = manga.FirstOrDefault(m => m.Id == 1);
        var manga2 = manga.FirstOrDefault(m => m.Id == 2);
        var mangaCol = manga.FirstOrDefault(m => m.Id == 3);

        manga1!.AddRelations(new()
        {
            { manga2!, CreationRelations.Parent },
            { mangaCol!, CreationRelations.Slave }
        });

        manga2!.AddRelations(new()
        {
            { manga1, CreationRelations.Child },
            { mangaCol!, CreationRelations.Slave }
        });

        mangaCol!.AddRelations(new()
        {
            { manga1, CreationRelations.Master },
            { manga2, CreationRelations.Master }
        });

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(1000)]
    public void ReadAuthorsTest()
    {
        using var db = new DatabaseContext();

        var authors = db.Authors.Include(a => a.AuthorsNames)
                                .Include(a => a.Circles)
                                .Include(a => a.AuthorsCreations)
                                .ThenInclude(ac => ac.Related)
                                .Include(a => a.CreaturesNames)
                                .Include(a => a.Tags)
                                .Include(a => a.CreaturesRelations)
                                .ThenInclude(cr => cr.Related)
                                .ToList();

        SerializeEntity(authors);
    }

    [Test]
    [Order(1000)]
    public void ReadCharactersTest()
    {
        using var db = new DatabaseContext();

        var characters = db.Characters.Include(c => c.CreaturesNames)
                                      .Include(c => c.CreationsCharacters)
                                      .ThenInclude(cc => cc.Origin)
                                      .Include(c => c.Tags)
                                      .Include(c => c.CreaturesRelations)
                                      .ThenInclude(cr => cr.Related)
                                      .ToList();

        SerializeEntity(characters);
    }

    [Test]
    [Order(1000)]
    public void ReadCirclesTest()
    {
        using var db = new DatabaseContext();

        var circles = db.Circles.Include(c => c.CirclesTitles)
                                .Include(c => c.Authors)
                                .Include(c => c.Creations)
                                .Include(c => c.Tags)
                                .ToList();

        SerializeEntity(circles);
    }

    [Test]
    [Order(1000)]
    public void ReadMangaTest()
    {
        using var db = new DatabaseContext();

        var manga = db.Manga.Include(m => m.CreationsRelations)
                            .Include(m => m.CreationsTitles)
                            .Include(m => m.AuthorsCreations)
                            .ThenInclude(ac => ac.Origin)
                            .Include(m => m.Circles)
                            .Include(m => m.CreationsCharacters)
                            .ThenInclude(cc => cc.Related)
                            .Include(m => m.Tags)
                            .ToList();

        SerializeEntity(manga);
    }

    [Test]
    [Order(1000)]
    public void ReadTagsTest()
    {
        using var db = new DatabaseContext();

        var tags = db.Tags.Include(t => t.Creatures)
                          .Include(t => t.Circles)
                          .Include(t => t.Creations)
                          .ToList();

        SerializeEntity(tags);
    }

    #endregion

    private static void SerializeEntity<T>(IEnumerable<T> entity) where T : class
    {
        var options = Essential.JsonSerializerOptions;
        var json = JsonSerializer.Serialize(entity, options);

        File.WriteAllText($"../{typeof(T)}.json", json);
    }
}

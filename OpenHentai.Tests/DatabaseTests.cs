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

        // init tag for basic categories

        var ycTag = new Tag(TagCategory.Parody, "Yuru Camp");
        ycTag.Description.Add(new("en-US::Anime about camping"));
        ycTag.Description.Add(new("en-US::()[[138-841/\"gaga\"/n\n\r\t\t/t/r/n]])"));
        ycTag.Description.Add(new("ru-RU::Аниме про кемпинг"));

        var yc2Tag = new Tag(TagCategory.Parody, "Yuru Camp Season 2");
        yc2Tag.Description.Add(new("en-US::Second season of Yuru Camp"));

        var jjbaTag = new Tag(TagCategory.Parody, "Jojo Bizzare Adventures");
        jjbaTag.Description.Add(new("First part of JJBA saga", "default"));

        var circleTag = new Tag(TagCategory.Personality, "Lazy");
        circleTag.Description.Add(new("en-US::This circle is lazy to release new chapters"));

        var authorTag = new Tag(TagCategory.BodyType, "Adult");
        authorTag.Description.Add(new("en-US::This author is adult"));

        var charaTag = new Tag(TagCategory.Species, "Human");
        charaTag.Description.Add(new("en-US::This character is human"));

        // set master-slave realtions

        yc2Tag.Master = ycTag;

        db.Tags.AddRange(ycTag, yc2Tag, jjbaTag, circleTag, authorTag, charaTag);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsTest()
    {
        using var db = new DatabaseContext();

        // TODO: move this into it's own test -> decrease this order to 1 since to dependencies
        // get author tag by searching for BodyType category
        var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.BodyType);

        // TODO: test default lang string (not here)
        var ycAuthor = new Author("en-US::Afro")
        {
            Birthday = new(2000, 01, 01),
            Age = 30,
            Gender = Gender.Male
        };
        ycAuthor.AddAuthorName(new("ja-JP::あｆろ"));
        ycAuthor.Description.Add(new("en-US::Author of Yuru Camp manga"));
        ycAuthor.Media.Add(new("https://cdn.myanimelist.net/images/anime/4/89877.jpg", MediaType.Image));
        ycAuthor.Tags.Add(tag!);
        ycAuthor.ExternalLinks.Add(new("twitter", "https://twitter.com/afro_2021")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free
        });

        var jjbaAuthor = new Author("en-US::Araki")
        {
            Birthday = new(1999, 09, 09),
            Age = 99,
            Gender = Gender.Male
        };
        jjbaAuthor.AddAuthorName(new("ja-JP::あらき"));
        jjbaAuthor.Description.Add(new("en-US::Author of JJBA manga"));
        jjbaAuthor.Media.Add(new("https://cdn.myanimelist.net/images/anime/4/89877.jpg", MediaType.Image));
        jjbaAuthor.Tags.Add(tag!);
        jjbaAuthor.ExternalLinks.Add(new("twitter", "https://twitter.com/afro_2021")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free
        });

        db.Authors.AddRange(ycAuthor, jjbaAuthor);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersTest()
    {
        using var db = new DatabaseContext();

        var tag = db.Tags.FirstOrDefault(t => t.Category == TagCategory.Species && t.Value == "Human");

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
        var tag = db.Tags.FirstOrDefault(t => t.Value == "Lazy");

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

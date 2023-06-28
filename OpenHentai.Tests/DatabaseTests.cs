using OpenHentai.Creations;
using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using Microsoft.Data.Sqlite;

namespace OpenHentai.Tests;

public class DatabaseTests
{
    private static readonly SqliteConnection _sqliteConnection = new($"Data Source={DatabasePath}");

    private const string DatabasePath = ":memory:";

    private static readonly DbContextOptions<DatabaseContext> _contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(_sqliteConnection).Options;

    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        await _sqliteConnection.OpenAsync().ConfigureAwait(false);

        using var db = new DatabaseContext(_contextOptions);

        await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }

    [OneTimeTearDown]
    public async Task CleanUp()
    {
        await _sqliteConnection.CloseAsync().ConfigureAwait(false);
    }

    #region Push tests

    #region Push without dependencies

    [Test]
    [Order(1)]
    public void PushAuthorsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        // using templates for unknown values
        var ym = new Author("default::Yukino Minato")
        {
            Birthday = new(1900, 01, 01),
            Age = 999,
            Gender = Gender.Female
        };
        ym.AddAuthorName("ja-JP::雪野 みなと");
        ym.AddName("default::Yukinominato");
        ym.Description.Add(new("en-US::Popular loli doujinshi artist"));
        ym.Media.Add(new("https://pbs.twimg.com/profile_images/1587354886751993856/vSCQYP59_400x400.jpg", MediaType.Image));
        ym.ExternalLinks.Add(new("twitter", "https://twitter.com/straycat_2018")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free
        });
        ym.ExternalLinks.Add(new("fanbox", "https://noraneko-no-tama.fanbox.cc/")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });

        var asanagi = new Author("default::Asanagi")
        {
            Birthday = new(1900, 01, 01),
            Age = 999,
            Gender = Gender.Male
        };
        asanagi.AddAuthorName("ja-JP::朝凪");
        asanagi.AddName("default::asanagi");
        asanagi.Description.Add(new("en-US::Popular mindbreak artist"));
        asanagi.Media.Add(new("https://pbs.twimg.com/profile_images/991625674757570562/MHkJ_qqa_400x400.jpg", MediaType.Image));
        asanagi.ExternalLinks.Add(new("twitter", "https://twitter.com/Victim_Girls")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free
        });
        asanagi.ExternalLinks.Add(new("fantia", "https://fantia.jp/asanagi")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });

        db.Authors.AddRange(ym, asanagi);

        db.SaveChanges();
    }

    [Test]
    [Order(1)]
    public void PushCirclesTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var nnntCircle = new Circle("default::noraneko-no-tama");
        nnntCircle.AddTitle("ja-JP::ノラネコノタマ");

        var fCircle = new Circle("default::Fatalpulse");

        db.Circles.AddRange(nnntCircle, fCircle);

        db.SaveChanges();
    }

    [Test]
    [Order(1)]
    public void PushMangaTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        // descriptions and metadata taken from toranoana/melonbooks/etc

        var ymManga1 = new Manga("default::Monokemono Shoya")
        {
            Length = 18,
            Volumes = 1,
            Chapters = 1,
            HasImages = true,
            PublishStarted = new DateTime(2012, 12, 28),
            PublishEnded = new DateTime(2012, 12, 28),
            Rating = Rating.R18,
            Status = PublishStatus.Published
        };
        ymManga1.AddTitle("ja-JP::ものけもの 初夜");
        ymManga1.ColoredInfo.Add(new(Color.BlackWhite, true));
        ymManga1.Languages.Add(new("ja-JP"));
        ymManga1.Languages.Add(new("ru-RU", false));
        ymManga1.Sources.Add(new("melonbooks", "https://www.melonbooks.co.jp/detail/detail.php?product_id=244658")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });
        ymManga1.Media.Add(new("https://melonbooks.akamaized.net/user_data/packages/resize_image.php?image=990000160064.jpg", MediaType.Image));
        ymManga1.Censorship.Add(new(Censorship.Tank, true));
        ymManga1.Censorship.Add(new(Censorship.None, false));
        ymManga1.Description.Add(new("ja-JP::サークル「ノラネコノタマ」オリジナル新シリーズ第一弾!いわくつきの物件での新生活。訪れるのは幸か不幸か・・・。世にも奇妙でほんとはえろい話。"));

        var ymManga2 = new Manga("default::Monokemono")
        {
            Length = 225,
            Volumes = 1,
            Chapters = 11,
            HasImages = true,
            PublishStarted = new DateTime(2012, 12, 28),
            PublishEnded = new DateTime(2014, 11, 1),
            Rating = Rating.R18,
            Status = PublishStatus.Published
        };
        ymManga2.AddTitle("ja-JP::ものけもの 妖児艶童怪異譚");
        ymManga2.ColoredInfo.Add(new(Color.BlackWhite, true));
        ymManga2.Languages.Add(new("ja-JP"));
        ymManga2.Sources.Add(new("toranoana", "https://ec.toranoana.jp/tora_r/ec/item/200011974236/")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });
        ymManga2.Media.Add(new("https://ecdnimg.toranoana.jp/ec/img/20/0011/97/42/200011974236-1p.jpg", MediaType.Image));
        ymManga2.Censorship.Add(new(Censorship.Tank, true));
        ymManga2.Description.Add(new("""
            ja-JP::
            妖しき少女とみだらな行為に耽る日々。
            妖と交わり、妖に犯され・・・この世のものとは思えない気持ちよさ♥
            ロリ系で圧倒的な人気の「ノラネコノタマ」作品集！！
            新作描き下ろしも含んだ単行本化シリーズ第３弾！！
        """));

        var asanagiManga1 = new Manga("default::VictimGirls 24")
        {
            Length = 32,
            Volumes = 1,
            Chapters = 1,
            HasImages = true,
            PublishStarted = new DateTime(2017, 12, 31),
            PublishEnded = new DateTime(2017, 12, 31),
            Rating = Rating.R18,
            Status = PublishStatus.Published
        };
        asanagiManga1.AddTitle("ja-JP::VictimGirls24　クソ生意気なドS娘に睡眠薬を");
        asanagiManga1.ColoredInfo.Add(new(Color.BlackWhite, true));
        asanagiManga1.Languages.Add(new("ja-JP"));
        asanagiManga1.Languages.Add(new("ru-RU", false));
        asanagiManga1.Sources.Add(new("toranoana", "https://ec.toranoana.jp/tora_r/ec/item/040030597248/")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });
        asanagiManga1.Media.Add(new("https://ecdnimg.toranoana.jp/ec/img/04/0030/59/72/040030597248-1p.jpg", MediaType.Image));
        asanagiManga1.Censorship.Add(new(Censorship.Tank, true));
        asanagiManga1.Censorship.Add(new(Censorship.None, false));
        asanagiManga1.Description.Add(new("""
            ja-JP::
            サークル【Fatalpulse】からコミケ93新刊[アズールレーン]本！
            『VictimGirls23 クソ生意気なドS娘に睡眠薬を』をご紹介。

            日頃から高飛車な態度が目につくエイジャックス。
            多少の無礼はともかく彼女は上官である指揮官を子豚呼ばわりし、
            挙句、彼の服を隠して他の艦船少女の前で恥をかかせた。

            度が過ぎる屈辱。自尊心を踏みにじられて湧き上がった怒りは治まらず、
            夜深く心に黒い衝動を抱きながら、指揮官は足音をそっと忍ばせる。

            息を殺して扉を開ければそこには薬で眠りに落ちたエイジャックス。

            どちらが上なのか教えてやる。
            寝息をたてるエイジャックスの柔肌を前に指揮官の情動が熱く滾る……。

            クソ生意気なドS娘・エイジャックスが痴態を晒すさまに興奮必至。
            実用度バツグンのいっさつとなっておりますのでどうぞお見逃しなく。
        """));

        var asanagiManga2 = new Manga("default::VictimGirls 25")
        {
            Length = 32,
            Volumes = 1,
            Chapters = 1,
            HasImages = true,
            PublishStarted = new DateTime(2018, 08, 12),
            PublishEnded = new DateTime(2018, 08, 12),
            Rating = Rating.R18,
            Status = PublishStatus.Published
        };
        asanagiManga2.AddTitle("ja-JP::VictimGirls25　デカ乳低身長種族♀の角を折る話");
        asanagiManga2.ColoredInfo.Add(new(Color.BlackWhite, true));
        asanagiManga2.Languages.Add(new("ja-JP"));
        asanagiManga2.Languages.Add(new("ru-RU", false));
        asanagiManga2.Sources.Add(new("toranoana", "https://ec.toranoana.jp/tora_r/ec/item/040030655691/")
        {
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Paid
        });
        asanagiManga2.Media.Add(new("https://ecdnimg.toranoana.jp/ec/img/04/0030/65/56/040030655691-1p.jpg", MediaType.Image));
        asanagiManga2.Censorship.Add(new(Censorship.Tank, true));
        asanagiManga2.Censorship.Add(new(Censorship.None, false));
        asanagiManga2.Description.Add(new("""
            ja-JP::
            サークル【Fatalpulse】が贈るコミケ94の新刊
            [グランブルーファンタジー]本の『VictimGirls25　デカ乳低身長種族♀の角を折る話』をご紹介致します。

            「待ってろよぉもう一本折ったら次はお前だからな」
            ドラフの女の子達を並べて角をノコギリで折っていくならず者の男。
            そこへラスティナが助けに来るのだが、敢え無く敗れてしまう。
            そんな彼女も角を折られ、所属している騎空団に箱詰めされて送り返されてしまう。

            ラスティナの敵討ちとばかりにならず者達の居る場所に向かうアリーザ達だったが
            そこで悲惨な光景を目の当たりにする。
            怒ったアリーザ達はならず者達を次々と倒していくのだが
            最後に現れたドラフの男に圧倒されてしまう事に……。
            そして敗北した彼女達はオナホにされ蹂躙されてしまう。
            果たして彼女達の運命は……！？

            続きはお手元にてお楽しみ下さい。
        """));

        db.Manga.AddRange(ymManga1, ymManga2, asanagiManga1, asanagiManga2);

        db.SaveChanges();
    }

    [Test]
    [Order(1)]
    public void PushCharactersTest()
    {
        using var db = new DatabaseContext(_contextOptions);

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

    [Test]
    [Order(1)]
    public void PushTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        // init tag for basic categories

        // author (related works), circle, character, manga
        var loliTag = new Tag(TagCategory.BodyType, "Loli");
        loliTag.Description.Add(new("en-US::Little girl"));
        loliTag.Description.Add(new("ru-RU::Маленькая девочка"));

        // character, manga
        var alTag = new Tag(TagCategory.Parody, "Azur Lane");
        alTag.Description.Add(new("en-US::Azur Lane parody tag"));

        // character, manga
        var gfTag = new Tag(TagCategory.Parody, "Granblue Fantasy");
        gfTag.Description.Add(new("en-US::Granblue Fantasy tag"));

        var al2Tag = new Tag(TagCategory.Parody, "azurlane");
        al2Tag.Description.Add(new("en-US::Alias for Azur Lane tag"));

        db.Tags.AddRange(loliTag, alTag, gfTag, al2Tag);

        db.SaveChanges();
    }

    #endregion

    #region Dependent pushes

    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushTagsRelationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var tags = db.Tags.ToHashSet();

        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var al2Tag = tags.FirstOrDefault(t => t.Value == "azurlane");

        al2Tag!.Master = alTag;

        db.SaveChanges();
    }

    // depends on PushAuthorsTest(1)
    // depends on PushCirclesTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsCirclesTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var circles = db.Circles.Include(c => c.Titles).ToHashSet();

        // or by searching through relative table:
        // var cts = db.CirclesTitles.ToHashSet();
        // var nnntCt = cts.FirstOrDefault(ct => ct.Text == "noraneko-no-tama");
        // var nnnt = circles.FirstOrDefault(c => c.Id == nnntCt.Id);

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        ym!.Circles.Add(nnnt!);
        fatalpulse!.Authors.Add(asanagi!);

        db.SaveChanges();
    }

    // depends on PushAuthorsTest(1)
    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsCreationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        ym!.AddCreation(ymM1!, AuthorRole.MainArtist);
        ym.AddCreation(ymM2!, AuthorRole.MainArtist);
        aM1!.AddAuthor(asanagi!, AuthorRole.MainArtist);
        aM2!.AddAuthor(asanagi!, AuthorRole.MainArtist);

        db.SaveChanges();
    }

    // depends on PushCharactersTest(1)
    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersCreationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

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

    // depends on PushTagsTest(1)
    // depends on PushAuthorsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        var tags = db.Tags.Include(tag => tag.Creatures).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        ym!.Tags.Add(loliTag!);
        alTag!.Creatures.Add(asanagi!);
        gfTag!.Creatures.Add(asanagi!);

        db.SaveChanges();
    }

    // depends on PushTagsTest(1)
    // depends on PushCharactersTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

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

    // depends on PushAuthorsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsRelationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        ym!.AddRelation(asanagi!, CreatureRelations.Unknown);
        asanagi!.AddRelation(ym, CreatureRelations.Friend);

        db.SaveChanges();
    }

    // depends on PushCharactersTest(1)
    [Test]
    [Order(2)]
    public void PushCharactersRelationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var characters = db.Characters.Include(a => a.Names).ToHashSet();

        var ymM1M = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Unnamed male"));
        var ymM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Akaname"));
        var aM1F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Ajax"));
        var aM2F = characters.FirstOrDefault(c => c.Names.Any(cn => cn.Text == "Aliza"));

        ymM1M!.AddRelation(ymM2F!, CreatureRelations.Unknown);
        aM1F!.AddRelation(aM2F!, CreatureRelations.Enemy);

        db.SaveChanges();
    }

    // depends on PushMangaTest(1)
    // depends on PushCirclesTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsCirclesTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        var circles = db.Circles.Include(c => c.Titles).Include(circle => circle.Creations).ToHashSet();

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        ymM1!.Circles.Add(nnnt!);
        ymM2!.Circles.Add(nnnt!);
        fatalpulse!.Creations.Add(aM1!);
        fatalpulse.Creations.Add(aM2!);

        db.SaveChanges();
    }

    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsRelationsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        ymM1!.AddRelation(ymM2!, CreationRelations.Slave);
        ymM2!.AddRelation(ymM1, CreationRelations.Master);
        aM1!.AddRelation(aM2!, CreationRelations.Parent);
        aM2!.AddRelation(aM1, CreationRelations.Child);

        db.SaveChanges();
    }

    // depends on PushMangaTest(1)
    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var manga = db.Manga.Include(m => m.Titles).ToHashSet();

        var ymM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono Shoya"));
        var ymM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "Monokemono"));
        var aM1 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 24"));
        var aM2 = manga.FirstOrDefault(m => m.Titles.Any(ct => ct.Text == "VictimGirls 25"));

        var tags = db.Tags.Include(tag => tag.Creations).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        ymM1!.Tags.Add(loliTag!);
        ymM2!.Tags.Add(loliTag!);
        loliTag!.Creations.Add(aM1!);
        alTag!.Creations.Add(aM1!);
        gfTag!.Creations.Add(aM2!);

        db.SaveChanges();
    }

    // depends on PushCirclesTest(1)
    // depends on PushTagsTest(1)
    [Test]
    [Order(2)]
    public void PushCirclesTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var circles = db.Circles.Include(c => c.Titles).Include(circle => circle.Creations).ToHashSet();

        var nnnt = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "noraneko-no-tama"));
        var fatalpulse = circles.FirstOrDefault(c => c.Titles.Any(ct => ct.Text == "Fatalpulse"));

        var tags = db.Tags.Include(tag => tag.Circles).ToHashSet();

        var loliTag = tags.FirstOrDefault(t => t.Value == "Loli");
        var alTag = tags.FirstOrDefault(t => t.Value == "Azur Lane");
        var gfTag = tags.FirstOrDefault(t => t.Value == "Granblue Fantasy");

        nnnt!.Tags.Add(loliTag!);
        loliTag!.Circles.Add(fatalpulse!);
        alTag!.Circles.Add(fatalpulse!);
        gfTag!.Circles.Add(fatalpulse!);

        db.SaveChanges();
    }

    #endregion

    #endregion

    #region Read tests

    [Test]
    [Order(10)]
    public void ReadAuthorsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames)
            .Include(a => a.Circles)
            .Include(a => a.Creations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.Names)
            .Include(a => a.Tags)
            .Include(a => a.Relations)
            .ThenInclude(cr => cr.Related);

        var json = SerializeEntity(authors);
        var deserialized = DeserializeEntity<IEnumerable<Author>>(json);
    }

    [Test]
    [Order(10)]
    public void ReadCharactersTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var characters = db.Characters.Include(c => c.Names)
                                      .Include(c => c.Creations)
                                      .ThenInclude(cc => cc.Origin)
                                      .Include(c => c.Tags)
                                      .Include(c => c.Relations)
                                      .ThenInclude(cr => cr.Related)
                                      .ToList();

        var json = SerializeEntity(characters);
        var deserialized = DeserializeEntity<IEnumerable<Character>>(json);
    }

    [Test]
    [Order(10)]
    public void ReadCirclesTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var circles = db.Circles.Include(c => c.Titles)
                                .Include(c => c.Authors)
                                .Include(c => c.Creations)
                                .Include(c => c.Tags)
                                .ToList();

        var json = SerializeEntity(circles);
        var deserialized = DeserializeEntity<IEnumerable<Circle>>(json);
    }

    [Test]
    [Order(10)]
    public void ReadMangaTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var manga = db.Manga.Include(m => m.Relations)
                            .Include(m => m.Titles)
                            .Include(m => m.Authors)
                            .ThenInclude(ac => ac.Origin)
                            .Include(m => m.Circles)
                            .Include(m => m.Characters)
                            .ThenInclude(cc => cc.Related)
                            .Include(m => m.Tags)
                            .ToList();

        var json = SerializeEntity(manga);
        var deserialized = DeserializeEntity<IEnumerable<Manga>>(json);
    }

    [Test]
    [Order(10)]
    public void ReadTagsTest()
    {
        using var db = new DatabaseContext(_contextOptions);

        var tags = db.Tags.Include(t => t.Creatures)
                          .Include(t => t.Circles)
                          .Include(t => t.Creations)
                          .ToList();

        var json = SerializeEntity(tags);
        var deserialized = DeserializeEntity<IEnumerable<Tag>>(json);
    }

    #endregion

    private static string SerializeEntity<T>(IEnumerable<T> entity) where T : class
    {
        var options = Essential.JsonSerializerOptions;
        var json = JsonSerializer.Serialize(entity, options);

        var jsonPath = $"../{typeof(T)}.json";
        // File.WriteAllText(jsonPath, json);

        return json;
    }

    private static T DeserializeEntity<T>(string json) where T : class
    {
        var options = Essential.JsonSerializerOptions;
        var entity = JsonSerializer.Deserialize<T>(json, options);

        return entity;
    }
}

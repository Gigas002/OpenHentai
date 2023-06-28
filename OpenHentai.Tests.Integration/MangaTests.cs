using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Statuses;

namespace OpenHentai.Tests.Integration;

public class MangaTests : DatabaseTestsBase
{
    [Test]
    [Order(1)]
    public void PushMangaTest()
    {
        using var db = new DatabaseContext(ContextOptions);

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

    // depends on PushMangaTest(1)
    [Test]
    [Order(2)]
    public void PushCreationsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

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

    [Test]
    [Order(10)]
    public async Task ReadMangaTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var manga = db.Manga.Include(m => m.Relations)
                            .Include(m => m.Titles)
                            .Include(m => m.Authors)
                            .ThenInclude(ac => ac.Origin)
                            .Include(m => m.Circles)
                            .Include(m => m.Characters)
                            .ThenInclude(cc => cc.Related)
                            .Include(m => m.Tags)
                            .ToList();

        var json = await SerializeEntityAsync(manga).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Manga>>(json).ConfigureAwait(false);
    }
}

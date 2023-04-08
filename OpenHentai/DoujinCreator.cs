using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Statuses;

namespace OpenHentai;

/// <summary>
/// ONLY FOR TESTING PURPOSES
/// </summary>
public static class DoujinCreator
{
    public static IAuthor CreateYukinoMinato()
    {
        var circle = new Circle
        {
            Title = "Noraneko no Tama",
            AlternativeTitles = new List<TitleInfo> { new("en-US:noranekonotama"), new("ja-JP:ノラネコノタマ") }
        };
        
        var author = new Author
        {
            Birthday = DateTime.Parse("01.01.1900"),
            Gender = Gender.Unknown,
            FirstName = "",
            LastName = "",
            FullName = "Yukino Minato",
            AlternativeNames = new List<string> { "雪野みなと" },
            Circles = new List<Circle> { circle },
            ExternalLinks = new List<ExternalLinkInfo>
            {
                new("https://twitter.com/straycat_2018"), new("https://www.pixiv.net/users/529489"),
                new("https://fantia.jp/fanclubs/493"), new("https://noraneko-no-tama.fanbox.cc/"),
                new("https://noraneko-no-tama.com/")
            },
            Description = "artist",
            Picture = new ImageInfo(new Uri("https://i.pximg.net/img-original/img/2022/09/16/19/13/43/101264803_p0.jpg")),
        };

        circle.Authors = new List<IAuthor> { author };

        return author;
    }

    public static IDoujinshi CreateMinatoDoujin()
    {
        var character = new Character
        {
            Age = 10,
            Gender = Gender.Female,
            FirstName = "FName",
            LastName = "LName",
            AlternativeNames = new List<string> { "AName" }
        };

        var externalLink = new ExternalLinkInfo("https://twitter.com/straycat_2018/status/1579417563091849217")
        {
            Title = "Test title",
            Link = new Uri("https://twitter.com/straycat_2018/status/1579417563091849217"),
            OfficialStatus = OfficialStatus.Official,
            PaidStatus = PaidStatus.Free
        };

        // var doujinTags = new List<ITag>
        // {
        //     new EnumTag<Genre>(Genre.Hentai),
        //     new EnumTag<Genre>(Genre.Drama),
        //     new EnumTag<Rating>(Rating.R18),
        //     new EnumTag<PublishStatus>(PublishStatus.Published)
        // };
        
        var doujinshi = new Doujinshi()
        {
            Title = "Totsuki tooka",
            AlternativeTitles = new List<TitleInfo> { new("ja-JP:とつきとおか") },
            PublishStarted = DateTime.Parse("08.04.2017"),
            PublishEnded = DateTime.Parse("08.04.2017"),
            AvailableAt = new List<ExternalLinkInfo> { new("https://t.co/YsyhsjRN1a"), new("https://t.co/Kyxl396wCp") },
            Description = "ロリ系大人気サークル「ノラネコノタマ」単行本シリーズ待望の第2弾!! 同級生の兄に初めてを奪われた少女。幼い子宮に幾度も幾度も精液を流し込まれ　やがてふくらみ始めた小さなお腹・・・もう引き返せない十月十日の時間が始まる。約100ページにも及ぶ大量の加筆修正!! そして新作描き下ろし!! 目の前で怯える少女を壊れるまで嬲るのか…それとも優しく愛でるのか・・・!?その瞳はただすがるように男を見つめている──…。",
            Characters = { { character, CharacterCreationRelation.MainCharacter } },
            Length = 196,
            Volumes = 1,
            Chapters = 5,
            Languages = new List<TranslationInfo>
            {
                new TranslationInfo("ja-JP", true),
                new TranslationInfo("en-US", false)
            },
            IsColored = false,
            Picture = new ImageInfo(new Uri("https://ebook-assets.dmm.co.jp/digital/e-book/b120ahit00706/b120ahit00706pl.jpg")),
            // Tags = doujinTags
        };

        return doujinshi;
    }
}

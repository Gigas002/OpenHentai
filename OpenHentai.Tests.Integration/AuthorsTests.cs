using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Statuses;

namespace OpenHentai.Tests.Integration;

public class AuthorsTests : DatabaseTestsBase
{
    #region Push tests

    [Test]
    [Order(1)]
    public void PushAuthorsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

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

    // depends on PushAuthorsTest(1)
    [Test]
    [Order(2)]
    public void PushAuthorsRelationsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames).ToHashSet();

        var ym = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Yukino Minato"));
        var asanagi = authors.FirstOrDefault(a => a.AuthorNames.Any(an => an.Text == "Asanagi"));

        ym!.AddRelation(asanagi!, CreatureRelations.Unknown);
        asanagi!.AddRelation(ym, CreatureRelations.Friend);

        db.SaveChanges();
    }

    #endregion

    #region Read tests

    [Test]
    [Order(10)]
    public async Task ReadAuthorsTest()
    {
        using var db = new DatabaseContext(ContextOptions);

        var authors = db.Authors.Include(a => a.AuthorNames)
            .Include(a => a.Circles)
            .Include(a => a.Creations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.Names)
            .Include(a => a.Tags)
            .Include(a => a.Relations)
            .ThenInclude(cr => cr.Related);

        var json = await SerializeEntityAsync(authors).ConfigureAwait(false);
        var deserialized = await DeserializeEntityAsync<IEnumerable<Author>>(json).ConfigureAwait(false);
    }

    #endregion
}

using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace OpenHentai.Database.Relative;

[Table("creatures_names")]
public class CreaturesNames : ILanguageSpecificTextInfoEntity<Creature>
{
    public ulong Id { get; set; }

    [ForeignKey("creature_id")]
    public Creature Entity { get; set; } = null!;

    [Column("name")]
    public string Text { get; set; } = null!;

    public string? Language { get; set; } = null!;

    public CreaturesNames() { }

    public CreaturesNames(Creature creature, string name, string? language) =>
        (Entity, Text, Language) = (creature, name, language);

    public CreaturesNames(Creature creature, string name, CultureInfo? language) :
        this(creature, name, language?.ToString()) { }

    public CreaturesNames(Creature creature, LanguageSpecificTextInfo name) :
        this(creature, name.Text, name.Language) { }

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() =>
        new LanguageSpecificTextInfo(Language, Text);
}

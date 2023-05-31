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

    public CreaturesNames(string name, string? language) => (Text, Language) = (name, language);

    public CreaturesNames(string name, CultureInfo? language) : this(name, language?.ToString()) { }

    public CreaturesNames(LanguageSpecificTextInfo name) : this(name.Text, name.Language) { }
}

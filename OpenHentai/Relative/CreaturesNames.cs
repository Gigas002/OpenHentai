using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Relative;

public class CreaturesNames : ILanguageSpecificTextInfoEntity<Creature>
{
    #region Properties

    public ulong Id { get; init; }

    [ForeignKey("creature_id")]
    [JsonIgnore]
    public Creature Entity { get; set; } = null!;

    [Column("name")]
    public string Text { get; set; } = null!;

    public string? Language { get; set; } = null!;

    #endregion

    #region Constructors

    public CreaturesNames() { }

    public CreaturesNames(Creature creature, string name, string? language) =>
        (Entity, Text, Language) = (creature, name, language);

    public CreaturesNames(Creature creature, string name, CultureInfo? language) :
        this(creature, name, language?.ToString()) { }

    public CreaturesNames(Creature creature, LanguageSpecificTextInfo name) :
        this(creature, name.Text, name.Language) { }

    #endregion

    #region Methods

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Text, Language);

    #endregion
}

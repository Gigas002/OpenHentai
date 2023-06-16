using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.Constants;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.JsonConverters;

namespace OpenHentai.Relative;

public class CreaturesNames : ILanguageSpecificTextInfoEntity<Creature>
{
    #region Properties

    public ulong Id { get; set; }

    [ForeignKey(FieldNames.CreatureId)]
    [JsonPropertyName(FieldNames.CreatureId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creature>))]
    public Creature Entity { get; set; } = null!;

    [Column(FieldNames.NameColumn)]
    [JsonPropertyName(FieldNames.NameColumn)]
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

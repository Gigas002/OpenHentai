using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Globalization;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Constants;
using OpenHentai.JsonConverters;

namespace OpenHentai.Relative;

public class CreationsTitles : ILanguageSpecificTextInfoEntity<Creation>
{
    #region Properties

    public ulong Id { get; set; }

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Entity { get; set; } = null!;

    [Column(FieldNames.TitleColumn)]
    [JsonPropertyName(FieldNames.TitleColumn)]
    public string Text { get; set; } = null!;
    
    public string? Language { get; set; } = null!;

    #endregion

    #region Constructors

    public CreationsTitles() { }

    public CreationsTitles(Creation creation, string name, string? language) =>
        (Entity, Text, Language) = (creation, name, language);

    public CreationsTitles(Creation creation, string name, CultureInfo? language) : this(creation, name, language?.ToString()) { }

    public CreationsTitles(Creation creation, LanguageSpecificTextInfo name) : this(creation, name.Text, name.Language) { }

    #endregion

    #region Methods

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Text, Language);

    #endregion
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Globalization;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Constants;
using OpenHentai.JsonConverters;

namespace OpenHentai.Relative;

public class CirclesTitles : ILanguageSpecificTextInfoEntity<Circle>
{
    #region Properties

    public ulong Id { get; set; }

    [ForeignKey(FieldNames.CircleId)]
    [JsonPropertyName(FieldNames.CircleId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Circle>))]
    public Circle Entity { get; set; } = null!;

    [Column(FieldNames.TitleColumn)]
    [JsonPropertyName(FieldNames.TitleColumn)]
    public string Text { get; set; } = null!;
    
    public string? Language { get; set; } = null!;

    #endregion

    #region Construtorcs

    public CirclesTitles() { }

    public CirclesTitles(Circle circle, string name, string? language) =>
        (Entity, Text, Language) = (circle, name, language);

    public CirclesTitles(Circle circle, string name, CultureInfo? language) : this(circle, name, language?.ToString()) { }

    public CirclesTitles(Circle circle, LanguageSpecificTextInfo name) : this(circle, name.Text, name.Language) { }

    #endregion

    #region Methods

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Text, Language);

    #endregion
}

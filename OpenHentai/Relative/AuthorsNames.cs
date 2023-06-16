using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Globalization;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Constants;
using OpenHentai.JsonConverters;

namespace OpenHentai.Relative;

public class AuthorsNames : ILanguageSpecificTextInfoEntity<Author>
{
    #region Properties

    public ulong Id { get; set; }
    
    [ForeignKey(FieldNames.AuthorId)]
    [JsonPropertyName(FieldNames.AuthorId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Author>))]
    public Author Entity { get; set; } = null!;

    [Column(FieldNames.NameColumn)]
    [JsonPropertyName(FieldNames.NameColumn)]
    public string Text { get; set; } = null!;
    
    public string? Language { get; set; }

    #endregion

    #region Construtorcs

    public AuthorsNames() { }

    public AuthorsNames(Author author, string name, string? language) =>
        (Entity, Text, Language) = (author, name, language);

    public AuthorsNames(Author author, string name, CultureInfo? language) : this(author, name, language?.ToString()) { }

    public AuthorsNames(Author author, LanguageSpecificTextInfo name) : this(author, name.Text, name.Language) { }

    #endregion

    #region Methods

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Text, Language);

    #endregion
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Globalization;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Relative;

public class AuthorsNames : ILanguageSpecificTextInfoEntity<Author>
{
    #region Properties

    public ulong Id { get; set; }

    [ForeignKey("author_id")]
    [JsonIgnore]
    public Author Entity { get; set; } = null!;

    [Column("name")]
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

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Language, Text);

    #endregion
}

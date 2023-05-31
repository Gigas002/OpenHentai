using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Relative;

[Table("authors_names")]
public class AuthorsNames : ILanguageSpecificTextInfoEntity<Author>
{
    public ulong Id { get; set; }

    [ForeignKey("author_id")]
    public Author Entity { get; set; } = null!;

    [Column("name")]
    public string Text { get; set; } = null!;
    public string? Language { get; set; }

    public AuthorsNames() { }

    public AuthorsNames(Author author, string name, string? language) =>
        (Entity, Text, Language) = (author, name, language);

    public AuthorsNames(Author author, string name, CultureInfo? language) : this(author, name, language?.ToString()) { }

    public AuthorsNames(Author author, LanguageSpecificTextInfo name) : this(author, name.Text, name.Language) { }

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() => new(Language, Text);
}

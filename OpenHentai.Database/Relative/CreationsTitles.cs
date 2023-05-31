using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Relative;

[Table("creations_titles")]
public class CreationsTitles : ILanguageSpecificTextInfoEntity<Creation>
{
    public ulong Id { get; set; }

    [ForeignKey("creation_id")]
    public Creation Entity { get; set; } = null!;

    [Column("title")]
    public string Text { get; set; } = null!;
    public string Language { get; set; } = null!;

    public CreationsTitles() { }

    public CreationsTitles(Creation creation, string name, string language) =>
        (Entity, Text, Language) = (creation, name, language);

    public CreationsTitles(Creation creation, string name, CultureInfo language) : this(creation, name, language.ToString()) { }

    public CreationsTitles(Creation creation, LanguageSpecificTextInfo name) : this(creation, name.Text, name.Language) { }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using OpenHentai.Database.Circles;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Relative;

[Table("circles_titles")]
public class CirclesTitles : ILanguageSpecificTextInfoEntity<Circle>
{
    public ulong Id { get; set; }

    [ForeignKey("circle_id")]
    public Circle Entity { get; set; }

    [Column("title")]
    public string Text { get; set; }
    public string Language { get; set; }

    public CirclesTitles() { }

    public CirclesTitles(Circle author, string name, string language) =>
        (Entity, Text, Language) = (author, name, language);

    public CirclesTitles(Circle author, string name, CultureInfo language) : this(author, name, language.ToString()) { }

    public CirclesTitles(Circle author, LanguageSpecificTextInfo name) : this(author, name.Text, name.Language) { }
}

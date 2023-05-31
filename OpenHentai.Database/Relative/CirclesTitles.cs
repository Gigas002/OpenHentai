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
    public Circle Entity { get; set; } = null!;

    [Column("title")]
    public string Text { get; set; } = null!;
    public string? Language { get; set; } = null!;

    public CirclesTitles() { }

    public CirclesTitles(Circle circle, string name, string? language) =>
        (Entity, Text, Language) = (circle, name, language);

    public CirclesTitles(Circle circle, string name, CultureInfo? language) : this(circle, name, language?.ToString()) { }

    public CirclesTitles(Circle circle, LanguageSpecificTextInfo name) : this(circle, name.Text, name.Language) { }

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo() =>
        new LanguageSpecificTextInfo(Language, Text);
}

using System.Globalization;

namespace OpenHentai.Descriptors;

/// <summary>
/// Describes alternative title's laguages
/// </summary>
public class TitleInfo
{
    /// <summary>
    /// Title value
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Title language
    /// </summary>
    public CultureInfo Language { get; set; }

    /// <summary>
    /// Create a new title
    /// </summary>
    /// <param name="title">String in format, e.g. "ja-JP::ポプテピピック"</param>
    public TitleInfo(string title)
    {
        // TODO: find out main title
        var titleCulture = title.Split("::");
        Title = titleCulture[1];
        // Invariant culture is selected by default, if lang not specified
        Language = new CultureInfo(titleCulture[0]);
    }

    /// <inheritdoc />
    public override string ToString() => Title;
}

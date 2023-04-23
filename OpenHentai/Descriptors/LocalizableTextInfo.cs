using System.Globalization;

namespace OpenHentai.Descriptors;

// TODO: probably not complete

/// <summary>
/// Localizable text in db's jsons (not indexed)
/// </summary>
public class LocalizableTextInfo
{
    /// <summary>
    /// Text language
    /// </summary>
    public CultureInfo Language { get; set; }

    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; }

    public LocalizableTextInfo(string formatedText)
    {
        // TODO: parse it here and set props
    }

    public LocalizableTextInfo(CultureInfo language, string text) => (Language, Text) = (language, text);
}
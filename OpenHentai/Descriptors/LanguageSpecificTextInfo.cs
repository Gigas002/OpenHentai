using System.Globalization;

namespace OpenHentai.Descriptors;

/// <summary>
/// Class for describing strings with language info,
/// including localizable text
/// </summary>
public class LanguageSpecificTextInfo
{
    /// <summary>
    /// Text language
    /// </summary>
    public CultureInfo Language { get; set; }
    
    /// <summary>
    /// Text on chosen language
    /// </summary>
    public string Text { get; set; }
    
    public LanguageSpecificTextInfo(string formatedText)
    {
        // TODO: parse it here and set props
    }

    public LanguageSpecificTextInfo(CultureInfo language, string text) => (Language, Text) = (language, text);
}

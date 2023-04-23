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
        var textLanguage = formatedText.Split("::");
        Text = textLanguage[1];
        // Invariant culture is selected by default, if lang not specified
        Language = textLanguage[0] == "default" || textLanguage.Length < 1 ?
            null : new CultureInfo(textLanguage[0]);
    }

    public LanguageSpecificTextInfo(CultureInfo language, string text) => (Language, Text) = (language, text);
}

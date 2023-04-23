using System.Globalization;

namespace OpenHentai.Descriptors;

/// <summary>
/// Class for describing strings with language info
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
}

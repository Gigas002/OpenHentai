using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

namespace OpenHentai.Descriptors;

/// <summary>
/// Class for describing strings with language info,
/// including localizable text
/// </summary>
public class LanguageSpecificTextInfo
{
    #region Properties

    /// <summary>
    /// Text language
    /// <para/> In case it's null - romanized value is passed
    /// </summary>
    [JsonConverter(typeof(CultureInfoJsonConverter))]
    public CultureInfo? Language { get; set; }
    
    /// <summary>
    /// Text on chosen language
    /// </summary>
    public string Text { get; set; } = null!;

    #endregion

    #region Constructors

    /// <summary>
    /// Create new string with language info
    /// </summary>
    public LanguageSpecificTextInfo() { }

    /// <summary>
    /// Create new string with language info
    /// </summary>
    /// <param name="formatedText">Formatted text line</param>
    public LanguageSpecificTextInfo(string formatedText)
    {
        var textLanguage = formatedText.Split("::");
        Text = textLanguage[1];
        // TODO: set prop by default and throw otherwise
        // Invariant culture is selected by default, if lang not specified
        Language = textLanguage[0] == "default" || textLanguage.Length < 1 ?
            null : new CultureInfo(textLanguage[0]);
    }

    // TODO: Consider changing the order of arguments, so everywhere will be
    // (text,lang) or (lang,text)

    /// <summary>
    /// Create new string with language info
    /// </summary>
    /// <param name="language">Line's culture/language</param>
    /// <param name="text">Line</param>
    public LanguageSpecificTextInfo(CultureInfo? language, string text) => (Language, Text) = (language, text);
    
    public LanguageSpecificTextInfo(string language, string text) : this(new CultureInfo(language), text) { }

    // TODO: const for "default" string

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        var language = Language is null ? "default" : Language.ToString();

        return $"{language}::{Text}";
    }

    #endregion
}

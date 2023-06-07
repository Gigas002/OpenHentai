using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

namespace OpenHentai.Descriptors;

// TODO: consider changing the standard
// problem: arguments order '(text, lang)' doesn't match 'lang::text'


/// <summary>
/// Class for describing strings with language info,
/// including localizable text
/// </summary>
public class LanguageSpecificTextInfo
{
    #region Constants

    /// <summary>
    /// Language is not chosen, refer to default field values
    /// </summary>
    public const string DefaultLanguage = "default";

    /// <summary>
    /// Symbols to delim language from text
    /// </summary>
    public const string LanguageDelimiter = "::";

    #endregion
    
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
        var textLanguage = formatedText.Trim().Split(LanguageDelimiter);
        Text = textLanguage[1];
        Language = textLanguage[0] == DefaultLanguage ?
            null : new CultureInfo(textLanguage[0]);
    }

    /// <summary>
    /// Create new string with language info
    /// </summary>
    /// <param name="text">Line</param>
    /// <param name="language">Line's culture/language</param>
    public LanguageSpecificTextInfo(string text, CultureInfo? language) => (Text, Language) = (text, language);
    
    public LanguageSpecificTextInfo(string text, string language) :
        this($"{language}{LanguageDelimiter}{text}") { }

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        var language = Language is null ? DefaultLanguage : Language.ToString();

        return $"{language}{LanguageDelimiter}{Text}";
    }

    #endregion
}

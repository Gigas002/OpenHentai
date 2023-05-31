using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

// TODO: snake_case for props names in dotnet 8
// see: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonnamingpolicy.snakecaselower?view=net-8.0#system-text-json-jsonnamingpolicy-snakecaselower

namespace OpenHentai.Descriptors;

/// <summary>
/// Class for describing strings with language info,
/// including localizable text
/// </summary>
public class LanguageSpecificTextInfo
{
    /// <summary>
    /// Text language
    /// <para/> In case it's null - romanized value is passed
    /// </summary>
    [JsonPropertyName("language")]
    [JsonConverter(typeof(CultureInfoJsonConverter))]
    public CultureInfo? Language { get; set; }
    
    /// <summary>
    /// Text on chosen language
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;

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

    /// <summary>
    /// Create new string with language info
    /// </summary>
    /// <param name="language">Line's culture/language</param>
    /// <param name="text">Line</param>
    public LanguageSpecificTextInfo(CultureInfo language, string text) => (Language, Text) = (language, text);

    // TODO: const for "default" string

    /// <inheritdoc />
    public override string ToString()
    {
        var language = Language is null ? "default" : Language.ToString();

        return $"{language}::{Text}";
    }
}

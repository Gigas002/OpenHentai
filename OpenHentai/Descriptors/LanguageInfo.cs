using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

// TODO: snake_case for props names in dotnet 8
// see: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonnamingpolicy.snakecaselower?view=net-8.0#system-text-json-jsonnamingpolicy-snakecaselower

namespace OpenHentai.Descriptors;

/// <summary>
/// Information about translation
/// see: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo?view=net-7.0
/// also: https://support.microsoft.com/en-us/topic/country-region-and-language-codes-add36afe-804a-44f1-ae68-cfb9c9b72f8b
/// </summary>
public class LanguageInfo
{
    /// <summary>
    /// Language
    /// </summary>
    [JsonPropertyName("language")]
    [JsonConverter(typeof(CultureInfoJsonConverter))]
    public CultureInfo Language { get; set; }

    /// <summary>
    /// Is official?
    /// </summary>
    [JsonPropertyName("is_official")]
    public bool IsOfficial { get; set; }

    public LanguageInfo() { }

    /// <summary>
    /// Initialize new translation info
    /// </summary>
    /// <param name="culture">Culture, e.g. "en-US"</param>
    /// <param name="isOfficial">Is translation official?</param>
    public LanguageInfo(string culture, bool isOfficial = true)
    {
        Language = new CultureInfo(culture);
        IsOfficial = isOfficial;
    }

    /// <inheritdoc />
    public override string ToString() => Language.NativeName;
}

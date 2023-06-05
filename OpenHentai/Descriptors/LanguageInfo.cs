using System.Globalization;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

namespace OpenHentai.Descriptors;

/// <summary>
/// Information about translation
/// see: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo?view=net-7.0
/// also: https://support.microsoft.com/en-us/topic/country-region-and-language-codes-add36afe-804a-44f1-ae68-cfb9c9b72f8b
/// </summary>
public class LanguageInfo
{
    #region Properties

    /// <summary>
    /// Language
    /// </summary>
    [JsonConverter(typeof(CultureInfoJsonConverter))]
    public CultureInfo Language { get; set; } = null!;

    /// <summary>
    /// Is official?
    /// </summary>
    public bool IsOfficial { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize new language info
    /// </summary>
    public LanguageInfo() { }

    /// <summary>
    /// Initialize new language info
    /// </summary>
    /// <param name="culture">Culture, e.g. "en-US"</param>
    /// <param name="isOfficial">Is translation official?</param>
    public LanguageInfo(string culture, bool isOfficial = true)
    {
        Language = new CultureInfo(culture);
        IsOfficial = isOfficial;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString() => Language.ToString();

    #endregion
}

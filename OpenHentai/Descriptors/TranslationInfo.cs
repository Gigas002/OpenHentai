using System.Globalization;

namespace OpenHentai.Descriptors;

/// <summary>
/// Information about translation
/// see: https://learn.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo?view=net-7.0
/// also: https://support.microsoft.com/en-us/topic/country-region-and-language-codes-add36afe-804a-44f1-ae68-cfb9c9b72f8b
/// </summary>
public class TranslationInfo
{
    /// <summary>
    /// Language
    /// </summary>
    public CultureInfo Language { get; set; }

    /// <summary>
    /// Is official?
    /// </summary>
    public bool IsOfficial { get; set; }

    public TranslationInfo(string culture, bool isOfficial = true)
    {
        Language = new CultureInfo(culture);
        IsOfficial = isOfficial;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Language.NativeName;
    }
}

using System.Text.Json.Serialization;

namespace OpenHentai.Descriptors;

// TODO: snake_case for props names in dotnet 8
// see: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonnamingpolicy.snakecaselower?view=net-8.0#system-text-json-jsonnamingpolicy-snakecaselower

/// <summary>
/// Standard for json in non-indexed, localizable descriptions
/// </summary>
// TODO: WIP
public class DescriptionInfo
{
    /// <summary>
    /// Description's text
    /// </summary>
    [JsonPropertyName("description")]
    public IEnumerable<LanguageSpecificTextInfo> Text { get; set; }

    /// <summary>
    /// Create new description
    /// </summary>
    /// <param name="descriptionLine">Formatted description line</param>
    public DescriptionInfo(string descriptionLine)
    {
        Text = new[] { new LanguageSpecificTextInfo(descriptionLine) };
    }

    /// <summary>
    /// Create new description
    /// </summary>
    /// <param name="description">Collection of formatted description lines</param>
    public DescriptionInfo(IEnumerable<string> description)
    {
        Text = description.Select(line => new LanguageSpecificTextInfo(line));
    }

    public DescriptionInfo() { }
}

using System.Text.Json.Serialization;
using OpenHentai.Statuses;

// TODO: snake_case for props names in dotnet 8
// see: https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonnamingpolicy.snakecaselower?view=net-8.0#system-text-json-jsonnamingpolicy-snakecaselower

namespace OpenHentai.Descriptors;

/// <summary>
/// External links
/// e.g.: pixiv:https://...
/// </summary>
public class ExternalLinkInfo
{
    #region Properties

    /// <summary>
    /// Title, e.g. pixiv
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// Uri
    /// </summary>
    [JsonPropertyName("link")]
    public Uri Link { get; set; }

    /// <summary>
    /// Official or not
    /// </summary>
    [JsonPropertyName("official_status")]
    public OfficialStatus OfficialStatus { get; set; }

    /// <summary>
    /// Free or not
    /// </summary>
    [JsonPropertyName("paid_status")]
    public PaidStatus PaidStatus { get; set; }

    /// <summary>
    /// Description, e.g. author's official free pixiv
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    #endregion

    #region Constructors

    /// <summary>
    /// Create new link
    /// </summary>
    /// <param name="link">Link</param>
    public ExternalLinkInfo(string link) => Link = new Uri(link);

    public ExternalLinkInfo() { }

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString() => Link.ToString();

    #endregion
}

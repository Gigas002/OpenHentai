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
    public string Title { get; set; } = null!;

    /// <summary>
    /// Uri
    /// </summary>
    [JsonPropertyName("link")]
    public Uri Link { get; set; } = null!;

    /// <summary>
    /// Official or not
    /// </summary>
    [JsonPropertyName("official_status")]
    public OfficialStatus OfficialStatus { get; set; } = OfficialStatus.Unknown;

    /// <summary>
    /// Free or not
    /// </summary>
    [JsonPropertyName("paid_status")]
    public PaidStatus PaidStatus { get; set; } = PaidStatus.Unknown;

    /// <summary>
    /// Description, e.g. author's official free pixiv
    /// </summary>
    [JsonPropertyName("description")]
    // TODO: consider using LanguageSpecific stuff
    public string? Description { get; set; }
    
    #endregion

    #region Constructors
    
    /// <summary>
    /// Create new link
    /// </summary>
    public ExternalLinkInfo() { }

    /// <summary>
    /// Create new link
    /// </summary>
    /// <param name="title">Title</param>
    /// <param name="link">Link</param>
    public ExternalLinkInfo(string title, Uri link) => (Title, Link) = (title, link);

    /// <summary>
    /// Create new link
    /// </summary>
    /// <param name="title">Title</param>
    /// <param name="link">Link</param>
    public ExternalLinkInfo(string title, string link) : this(title, new Uri(link))
    { }

    #endregion
}

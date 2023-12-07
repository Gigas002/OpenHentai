using OpenHentai.Statuses;

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
    public string Title { get; set; } = null!;

    /// <summary>
    /// Uri
    /// </summary>
    public Uri Link { get; set; } = null!;

    // TODO: consider use bool instead
    /// <summary>
    /// Official or not
    /// </summary>
    public OfficialStatus OfficialStatus { get; set; } = OfficialStatus.Unknown;

    /// <summary>
    /// Free or not
    /// </summary>
    public PaidStatus PaidStatus { get; set; } = PaidStatus.Unknown;

    /// <summary>
    /// Description, e.g. author's official free pixiv
    /// </summary>
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = [];
    
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

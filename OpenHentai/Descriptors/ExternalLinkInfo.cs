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
    public string Title { get; set; }

    /// <summary>
    /// Uri
    /// </summary>
    public Uri Link { get; set; }

    /// <summary>
    /// Official or not
    /// </summary>
    public OfficialStatus OfficialStatus { get; set; }

    /// <summary>
    /// Free or not
    /// </summary>
    public PaidStatus PaidStatus { get; set; }

    /// <summary>
    /// Description, e.g. author's official free pixiv
    /// </summary>
    public string Description { get; set; }
    
    #endregion

    #region Constructors

    /// <summary>
    /// Create new link
    /// </summary>
    /// <param name="link">Link</param>
    public ExternalLinkInfo(string link)
    {
        Link = new Uri(link);
    }
    
    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString() => Link.ToString();

    #endregion
}

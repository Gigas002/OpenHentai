namespace OpenHentai.Statuses;

/// <summary>
/// Creation publishing status
/// </summary>
public enum PublishStatus
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Still publishing
    /// </summary>
    Publishing,
    
    /// <summary>
    /// Ended publishing
    /// </summary>
    Published,
    
    /// <summary>
    /// Release schedule on hold
    /// </summary>
    OnHold,
    
    /// <summary>
    /// Cancelled
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Not yet published
    /// </summary>
    NotYetPublished
}

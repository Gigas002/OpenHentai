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
    Publishing = 1,
    
    /// <summary>
    /// Ended publishing
    /// </summary>
    Published = 2,
    
    /// <summary>
    /// Release schedule on hold
    /// </summary>
    OnHold = 3,
    
    /// <summary>
    /// Cancelled
    /// </summary>
    Cancelled = 4,
    
    /// <summary>
    /// Not yet published
    /// </summary>
    NotYetPublished = 5
}

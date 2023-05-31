namespace OpenHentai.Statuses;

/// <summary>
/// Is officially free or not
/// </summary>
public enum PaidStatus
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Free
    /// </summary>
    Free,
    
    /// <summary>
    /// Requires payment
    /// </summary>
    Paid,
    
    /// <summary>
    /// Nowhere to buy or read for free officially
    /// </summary>
    Unavailable
}

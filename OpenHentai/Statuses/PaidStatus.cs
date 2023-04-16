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
    Free = 1,
    
    /// <summary>
    /// Requires payment
    /// </summary>
    Paid = 2,
    
    /// <summary>
    /// Nowhere to buy or read for free officially
    /// </summary>
    Unavailable = 3
}

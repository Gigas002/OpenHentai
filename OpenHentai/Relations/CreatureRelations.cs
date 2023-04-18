namespace OpenHentai.Relations;

/// <summary>
/// Relations between creatures
/// </summary>
public enum CreatureRelations
{
    /// <summary>
    /// Unknown or none
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Family ties
    /// </summary>
    Relative,
    
    /// <summary>
    /// Alternative version
    /// </summary>
    Alternative,
    
    /// <summary>
    /// Friendly character
    /// </summary>
    Friend,
    
    /// <summary>
    /// Non-friendly character
    /// </summary>
    Enemy
}

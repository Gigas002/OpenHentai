namespace OpenHentai.Relations;

/// <summary>
/// Relation to another creation
/// </summary>
public enum CreationRelations
{
    /// <summary>
    /// Unknown or none
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Parent story
    /// </summary>
    Parent = 1,
    
    /// <summary>
    /// Alternative story
    /// </summary>
    Alternative = 2,
    
    /// <summary>
    /// Child story
    /// </summary>
    Child = 3,
    
    /// <summary>
    /// Spin-off or alternative stories
    /// </summary>
    SpinOff = 4,
}

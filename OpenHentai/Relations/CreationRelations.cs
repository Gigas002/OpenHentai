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
    /// Spin-off or alternative stories
    /// </summary>
    Alternative = 2,
    
    /// <summary>
    /// Child story
    /// </summary>
    Child = 3,
    
    /// <summary>
    /// This is master in creation collection
    /// </summary>
    CollectionMaster = 4,
    
    /// <summary>
    /// This is a slave object in creation collection
    /// </summary>
    CollectionSlave = 5
}

namespace OpenHentai.Relations;

/// <summary>
/// Relations between character and creation
/// </summary>
public enum CharacterRole
{
    /// <summary>
    /// Unknown or none
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Main character
    /// </summary>
    Main,
    
    /// <summary>
    /// Supporting character
    /// </summary>
    Secondary,
    
    /// <summary>
    /// Cosplaying character
    /// e.g. Kagamihara Nadeshiko in KanColle Hibiki's cosplay means
    /// Hibiki character with CosplayCharacter property value
    /// and Nadeshiko with MainCharacter property value
    /// </summary>
    Cosplay,
}

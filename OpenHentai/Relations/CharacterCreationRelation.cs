namespace OpenHentai.Relations;

/// <summary>
/// Relations between character and creation
/// </summary>
public enum CharacterCreationRelation
{
    /// <summary>
    /// Unknown or none
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Main character
    /// </summary>
    MainCharacter,
    
    /// <summary>
    /// Supporting character
    /// </summary>
    SupportingCharacter,
    
    /// <summary>
    /// Cosplaying character
    /// e.g. Kagamihara Nadeshiko in KanColle Hibiki's cosplay means
    /// Hibiki character with CosplayCharacter property value
    /// and Nadeshiko with MainCharacter property value
    /// </summary>
    CosplayCharacter,
}

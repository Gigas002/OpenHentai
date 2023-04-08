namespace OpenHentai.Creatures;

/// <summary>
/// Body type of creature
/// e.g. lolibaba: Gender=female, Genitals=female, Age=500, BodyType=Loli
/// </summary>
[Obsolete("Use tags")]
public enum BodyType
{
    /// <summary>
    /// Unknown body type
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Lolita
    /// </summary>
    Loli = 1,
    
    /// <summary>
    /// Lolita with big tits
    /// </summary>
    OppaiLoli = 2,
}

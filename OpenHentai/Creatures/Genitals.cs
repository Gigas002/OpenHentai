namespace OpenHentai.Creatures;

/// <summary>
/// Related to <see cref="Gender"/>, e.g. Gender.Female + Genitals.Male = futanari
/// Also, see <see cref="BodyType"/>
/// </summary>
public enum Genitals
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Male
    /// </summary>
    Male = 1,
    
    /// <summary>
    /// Female
    /// </summary>
    Female = 2,
    
    /// <summary>
    /// No genitals
    /// </summary>
    None = 3,
    
    /// <summary>
    /// Both male and female
    /// </summary>
    Both = 4
}

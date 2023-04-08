namespace OpenHentai.Tags;

/// <summary>
/// Available tag categories
/// </summary>
public enum TagCategory
{
    /// <summary>
    /// Any unspecified tag category
    /// </summary>
    Other = 0,
    
    /// <summary>
    /// Related to some franchise
    /// </summary>
    Franchise,

    /// <summary>
    /// Creation themes
    /// More specific, than genres, e.g. Isekai, Lolicon, Rape, Glasses
    /// </summary>
    Theme,
    
    /// <summary>
    /// Creature body type
    /// e.g. lolibaba: Gender=female, Genitals=female, Age=500, BodyType=Loli (as string tag)
    /// </summary>
    BodyType
}

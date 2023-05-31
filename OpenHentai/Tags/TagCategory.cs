namespace OpenHentai.Tags;

/// <summary>
/// Available tag categories
/// </summary>
// TODO: complete tag enum
public enum TagCategory
{
    /// <summary>
    /// Any unspecified tag category
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Related to some franchise
    /// </summary>
    Parody,
    
    // WIP zone, see tags.md in wt repo

#pragma warning disable CS1591 // xml-docs

    AgeChange,
    BodyType,
    BodyChange,
    BodyHeight,
    BodySkin,
    BodyWeight,
    BodyHead,
    BodyHair,
    BodyEyes,
    BodyNose,
    BodyMouth,
    BodyNeck,
    BodyArms,
    BodyChest,
    BodyTorso,
    BodyCrotch,
    BodyGenitals,
    BodyLegs,
    Clothes,
    Personality,
    Species,
    SpeciesAnimal,
    Tools,
    Fluids,
    Force,
    Privacy,
    SelfPleasure,
    Consumption,
    GenderChange
}

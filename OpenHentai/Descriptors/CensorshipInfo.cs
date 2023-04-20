using OpenHentai.Creations;

namespace OpenHentai.Descriptors;

/// <summary>
/// Information about censorship in creation
/// </summary>
public class CensorshipInfo
{
    /// <summary>
    /// Censorship type
    /// </summary>
    public Censorship Censorship { get; set; }

    /// <summary>
    /// Is this kind of censorship official?
    /// </summary>
    public bool IsOfficial { get; set; }
}

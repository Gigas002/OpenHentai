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
    public Censorship Censorship { get; set; } = Censorship.Unknown;

    /// <summary>
    /// Is this kind of censorship official?
    /// </summary>
    public bool IsOfficial { get; set; }

    /// <summary>
    /// Create new CensorshipInfo object
    /// </summary>
    public CensorshipInfo() { }

    /// <summary>
    /// Create new CensorshipInfo object
    /// </summary>
    /// <param name="censorship">Censorship</param>
    /// <param name="isOfficial">Is official?</param>
    public CensorshipInfo(Censorship censorship, bool isOfficial) => (Censorship, IsOfficial) = (censorship, isOfficial);
}

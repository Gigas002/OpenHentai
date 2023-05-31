namespace OpenHentai.Roles;

/// <summary>
/// Author's role in creation
/// It is needed since different authors can make different kind of work
/// Plus, it makes search more powerful
/// </summary>
public enum AuthorRole
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Main artist
    /// </summary>
    MainArtist,
    
    /// <summary>
    /// Secondary artist (e.g. non-h chapters at the end of Comic LO)
    /// </summary>
    SecondaryArtist,
    
    /// <summary>
    /// Illustrator of main page (e.g. Comic LO covers)
    /// </summary>
    MainPageIllustrator
}

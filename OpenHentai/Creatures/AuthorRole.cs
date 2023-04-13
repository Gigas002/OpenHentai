namespace OpenHentai.Creatures;

/// <summary>
/// Author's role in creation
/// It is needed since different authors can make different kind of work
/// Plus, it makes search more powerful
/// </summary>
public enum AuthorRole
{
    /// <summary>
    /// Main artist
    /// </summary>
    MainArtist = 0,
    
    /// <summary>
    /// Secondary artist (e.g. non-h chapters at the end of Comic LO)
    /// </summary>
    SecondaryArtist = 1,
    
    /// <summary>
    /// Illustrator of main page (e.g. Comic LO covers)
    /// </summary>
    MainPageIllustrator = 2
}

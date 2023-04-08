namespace OpenHentai.Descriptors;

/// <summary>
/// External adaptations of creation
/// Since this is an external stuff, there should be as less information as possible
/// </summary>
public class AdaptationInfo
{
    /// <summary>
    /// Adaptation title
    /// </summary>
    public string Title { get; set; }

    // public IEnumerable<string> AlternativeTitles { get; set; }
    //
    // public string Description { get; set; }

    /// <summary>
    /// Adaptation link
    /// </summary>
    public Uri Link { get; set; }
}

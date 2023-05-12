using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Circles;

/// <summary>
/// Author's circle
/// </summary>
public interface ICircle
{
    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    public IEnumerable<LanguageSpecificTextInfo> Titles { get; set; }

    /// <summary>
    /// Related authors
    /// </summary>
    public IEnumerable<IAuthor> Authors { get; set; }

    /// <summary>
    /// Related creations
    /// </summary>
    public IEnumerable<ICreation> Creations { get; set; }
}

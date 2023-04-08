using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Circles;

/// <summary>
/// Author's circle
/// </summary>
public interface ICircle : IDatabaseEntry
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Alternative titles
    /// </summary>
    public IEnumerable<TitleInfo> AlternativeTitles { get; set; }

    /// <summary>
    /// Related authors
    /// </summary>
    public IEnumerable<IAuthor> Authors { get; set; }

    /// <summary>
    /// Related creations
    /// </summary>
    public IEnumerable<ICreation> Creations { get; set; }
}

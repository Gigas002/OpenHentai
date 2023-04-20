using OpenHentai.Creations;
using OpenHentai.Creatures;

namespace OpenHentai.Descriptors;

// TODO: early WIP
// handle non-doujin stuff, e.g. 2022-winter-set, etc. Hande prices

/// <summary>
/// Information about event's entry
/// </summary>
public class EventEntryInfo
{
    /// <summary>
    /// Author
    /// </summary>
    public ICreature Author { get; set; }

    /// <summary>
    /// Sold creations
    /// </summary>
    public IEnumerable<ICreation> Creations { get; set; }

    /// <summary>
    /// Was on event during dates
    /// </summary>
    public IEnumerable<DateTime> Dates { get; set; }

    /// <summary>
    /// Which place on map this autor reside
    /// </summary>
    public string Place { get; set; }
}

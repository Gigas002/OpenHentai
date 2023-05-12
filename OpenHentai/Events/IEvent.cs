using OpenHentai.Descriptors;

namespace OpenHentai.Events;

// TODO: complete IEvent interface

// Events are a big feature, not planned to work until some progress
// on a main part is done. It will be used into interactive event map,
// like Comiket's catalog

/// <summary>
/// IRL event, e.g. comiket, comic1, etc
/// </summary>
public interface IEvent
{
    /// <summary>
    /// Event's known titles
    /// </summary>
    public IEnumerable<string> Titles { get; set; }

    // StartDate and EndDate are not very clear, since there may be missing days
    // between them
    
    /// <summary>
    /// Event's dates
    /// </summary>
    public IEnumerable<DateTime> Dates { get; set; }

    /// <summary>
    /// Event's description
    /// </summary>
    public DescriptionInfo Description { get; set; }

    // original language or romanization?
    /// <summary>
    /// Event's address
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Event's entries
    /// </summary>
    public IEnumerable<EventEntryInfo> Entries { get; set; }
}

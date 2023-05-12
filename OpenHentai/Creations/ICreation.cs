using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Tags;

namespace OpenHentai.Creations;

/// <summary>
/// Creation, e.g. doujinshi, game, etc
/// </summary>
public interface ICreation
{
    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    public IEnumerable<LanguageSpecificTextInfo> Titles { get; set; }
    
    /// <summary>
    /// Authors
    /// </summary>
    public IDictionary<IAuthor, AuthorRole> Authors { get; init; }

    /// <summary>
    /// Estimate date of first release of this creation
    /// </summary>
    public DateTime PublishStarted { get; set; }
    
    /// <summary>
    /// Estimate date of final release of this creation
    /// </summary>
    public DateTime PublishEnded { get; set; }

    /// <summary>
    /// Available to purchase/read/etc at
    /// </summary>
    public IEnumerable<ExternalLinkInfo> Sources { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public DescriptionInfo Description { get; set; }
    
    /// <summary>
    /// Collection of related creations
    /// Creation-Relation pair
    /// </summary>
    public IDictionary<ICreation, CreationRelations> Relations { get; init; }

    // /// <summary>
    // /// Featured at events, e.g. C99, C100, etc
    // /// </summary>
    // public IEnumerable<IEvent> Events { get; set; }
    
    /// <summary>
    /// Member of collections
    /// </summary>
    public IEnumerable<ICreationCollection> Collections { get; set; }

    /// <summary>
    /// Collection of characters
    /// </summary>
    public IDictionary<ICharacter, CharacterRole> Characters { get; init; }

    /// <summary>
    /// Collection of related pictures, including preview image
    /// </summary>
    public IEnumerable<PictureInfo> Pictures { get; set; }

    /// <summary>
    /// Creation id in db
    /// </summary>
    public ulong CreationId { get; set; }
    
    /// <summary>
    /// Available on languages
    /// </summary>
    public IEnumerable<LanguageInfo> Languages { get; set; }
    
    /// <summary>
    /// Age rating
    /// </summary>
    public Rating Rating { get; set; }
    
    /// <summary>
    /// Publishing status
    /// </summary>
    public PublishStatus Status { get; set; }
    
    /// <summary>
    /// Censorship type
    /// </summary>
    public IEnumerable<CensorshipInfo> Censorship { get; set; }

    /// <summary>
    /// Creation's tags
    /// e.g. franchise parody, themes, etc
    /// </summary>
    public IEnumerable<ITag> Tags { get; set; }
}

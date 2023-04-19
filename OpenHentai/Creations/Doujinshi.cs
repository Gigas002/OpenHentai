using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Events;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Tags;

namespace OpenHentai.Creations;

/// <inheritdoc />
public class Doujinshi : IDoujinshi
{
    #region Properties

    #region Interfaces implementation

    /// <inheritdoc />
    public IEnumerable<TitleInfo> Titles { get; set; }
    
    /// <inheritdoc />
    public IDictionary<IAuthor, AuthorRole> Authors { get; init; }

    /// <inheritdoc />
    public DateTime PublishStarted { get; set; }
    
    /// <inheritdoc />
    public DateTime PublishEnded { get; set; }

    /// <inheritdoc />
    // TODO: rework
    public IEnumerable<ExternalLinkInfo> AvailableAt { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public IDictionary<ICreation, CreationRelations> Relations { get; init; }
    
    // /// <inheritdoc />
    // TODO: events
    // public IEnumerable<IEvent> FeaturedAtEvents { get; set; }
    
    /// <inheritdoc />
    // TODO: consider rework
    public IEnumerable<ICreationCollection> MemberOfCollections { get; set; }
    
    /// <inheritdoc />
    public IDictionary<ICharacter, CharacterRole> Characters { get; init; }

    /// <inheritdoc />
    // TODO: CensorshipInfo
    public Censorship Censorship { get; set; }

    /// <inheritdoc />
    public IEnumerable<ITag> Tags { get; set; }

    /// <inheritdoc />
    // TODO: slightly change
    public IEnumerable<TranslationInfo> Languages { get; set; }
    
    /// <inheritdoc />
    public Rating Rating { get; set; }

    /// <inheritdoc />
    public PublishStatus Status { get; set; }

    /// <inheritdoc />
    public IEnumerable<PictureInfo> Pictures { get; set; }
    
    /// <inheritdoc />
    public int Length { get; set; }
    
    /// <inheritdoc />
    public int Volumes { get; set; }
    
    /// <inheritdoc />
    public int Chapters { get; set; }

    /// <inheritdoc />
    public bool HasImages { get; set; } = true;

    /// <inheritdoc />
    public IEnumerable<ColoredInfo> ColoredInfo { get; set; }
    
    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong CreationId { get; set; }
    
    #endregion

    #endregion
}

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
    public string Title { get; set; }
    
    /// <inheritdoc />
    // TODO: refer to a new tagging guide to rewrite this
    public IEnumerable<TitleInfo> AlternativeTitles { get; set; }
    
    /// <inheritdoc />
    public IDictionary<IAuthor, AuthorRole> Authors { get; init; }

    /// <inheritdoc />
    public DateTime PublishStarted { get; set; }
    
    /// <inheritdoc />
    public DateTime PublishEnded { get; set; }

    /// <inheritdoc />
    public IEnumerable<ExternalLinkInfo> AvailableAt { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public IDictionary<ICreation, CreationRelations> Relations { get; init; }
    
    /// <inheritdoc />
    public IEnumerable<IEvent> FeaturedAtEvents { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<ICreationCollection> MemberOfCollections { get; set; }
    
    /// <inheritdoc />
    public IDictionary<ICharacter, CharacterRole> Characters { get; init; }

    /// <inheritdoc />
    // TODO: consider move to tags
    public Censorship Censorship { get; set; }

    /// <inheritdoc />
    public IEnumerable<ITag> Tags { get; set; }

    /// <inheritdoc />
    // TODO: consider move to tags
    public IEnumerable<AdaptationInfo> Adaptations { get; set; }

    /// <inheritdoc />
    // TODO: consider move to tags
    public IEnumerable<TranslationInfo> Languages { get; set; }

    /// <inheritdoc />
    // TODO: consider move to tags
    public Rating Rating { get; set; }
    
    /// <inheritdoc />
    // TODO: consider move to tags
    public IEnumerable<Genre> Genres { get; set; }
    
    /// <inheritdoc />
    public PublishStatus Status { get; set; }

    /// <inheritdoc />
    public PictureInfo Picture { get; set; }
    
    /// <inheritdoc />
    public int Length { get; set; }
    
    /// <inheritdoc />
    public int Volumes { get; set; }
    
    /// <inheritdoc />
    public int Chapters { get; set; }

    /// <inheritdoc />
    public bool HasImages { get; set; } = true;

    /// <inheritdoc />
    public bool IsColored { get; set; }
    
    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong CreationId { get; set; }
    
    #endregion

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString() => Title;

    #endregion
}

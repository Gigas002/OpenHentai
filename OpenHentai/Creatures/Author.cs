using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Tags;

namespace OpenHentai.Creatures;

/// <inheritdoc />
public class Author : IAuthor
{
    #region Properties

    #region Interfaces implementation

    /// <inheritdoc />
    public string FirstName { get; set; }
    
    /// <inheritdoc />
    public string LastName { get; set; }
    
    /// <inheritdoc />
    public string FullName { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<string> AlternativeNames { get; set; }

    /// <inheritdoc />
    public IEnumerable<string> AdditionalDetails { get; set; }
    
    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public DateTime Birthday { get; set; }
    
    /// <inheritdoc />
    public int Age { get; set; }

    /// <inheritdoc />
    public ImageInfo Picture { get; set; }

    /// <inheritdoc />
    public string Species { get; set; }
    
    /// <inheritdoc />
    public Gender Gender { get; set; }
    
    // /// <inheritdoc />
    // public BodyType BodyType { get; set; }

    /// <inheritdoc />
    public IEnumerable<ITag> Tags { get; set; }
    
    /// <inheritdoc />
    public string AuthorName { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<string> AlternativeAuthorNames { get; set; }

    /// <inheritdoc />
    public IEnumerable<ICircle> Circles { get; set; }

    /// <inheritdoc />
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <inheritdoc />
    public IDictionary<ICreation, string> Creations { get; set; }

    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong CreatureId { get; set; }

    /// <inheritdoc />
    public IDictionary<ICreature, CreatureRelations> Relations { get; set; }
    
    /// <inheritdoc />
    public Genitals Genitals { get; set; }

    #endregion

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        return FullName;
    }
    
    #endregion
}

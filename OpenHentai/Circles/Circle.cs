using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Circles;

/// <inheritdoc />
public class Circle : ICircle
{
    #region Properties

    #region Interfaces implementation

    /// <inheritdoc />
    public string Title { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<TitleInfo> AlternativeTitles { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<IAuthor> Authors { get; set; }

    /// <inheritdoc />
    public IEnumerable<ICreation> Creations { get; set; }

    /// <inheritdoc />
    public ulong Id { get; set; }
    
    #endregion
    
    #endregion

    #region Constructors

    

    #endregion

    #region Methods

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
    
    #endregion
}

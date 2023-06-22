using OpenHentai.Creations;

namespace OpenHentai.Contexts;

public class MangaContextHelper : CreationsContextHelper<Manga>
{
    #region Constructors

    public MangaContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Manga> GetManga() => Context.Manga;

    #endregion

    #endregion
}

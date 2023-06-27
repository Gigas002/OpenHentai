using OpenHentai.Creations;

namespace OpenHentai.Repositories;

public class MangaRepository : DatabaseRepository, IMangaRepository
{
    #region Constructors

    public MangaRepository(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Manga> GetManga() => Context.Manga;

    #endregion

    #endregion
}

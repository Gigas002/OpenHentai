using OpenHentai.Creations;

namespace OpenHentai.Repositories;

public interface IMangaRepository : ICreationsRepository<Manga>
{
    public IEnumerable<Manga> GetManga();
}

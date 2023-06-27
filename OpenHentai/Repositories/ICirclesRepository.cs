using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Repositories;

public interface ICirclesRepository : IDatabaseRepository
{
    public IEnumerable<Circle> GetCircles();
    public IEnumerable<CirclesTitles> GetAllTitles();
    public Task<IEnumerable<CirclesTitles>?> GetTitlesAsync(ulong id);
    public Task<IEnumerable<Author>?> GetAuthorsAsync(ulong id);
    public Task<IEnumerable<Creation>?> GetCreationsAsync(ulong id);
    public Task<IEnumerable<Tag>?> GetTagsAsync(ulong id);
    public Task<bool> AddTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles);
    public Task<bool> AddAuthorsAsync(ulong id, HashSet<ulong> authorsIds);
    public Task<bool> AddCreationsAsync(ulong id, HashSet<ulong> creationsIds);
    public Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds);
    public Task<bool> RemoveTitlesAsync(ulong id, HashSet<ulong> titleIds);
    public Task<bool> RemoveAuthorsAsync(ulong id, HashSet<ulong> authorIds);
    public Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds);
    public Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds);
}

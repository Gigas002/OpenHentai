using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Tags;

namespace OpenHentai.Repositories;

public interface ITagsRepository : IDatabaseRepository
{
    public IEnumerable<Tag> GetTags();
    public Task<IEnumerable<Creature>?> GetCreaturesAsync(ulong id);
    public Task<IEnumerable<Creation>?> GetCreationsAsync(ulong id);
    public Task<IEnumerable<Circle>?> GetCirclesAsync(ulong id);
    public Task<bool> AddCreaturesAsync(ulong id, HashSet<ulong> creatureIds);
    public Task<bool> AddCreationsAsync(ulong id, HashSet<ulong> creationIds);
    public Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds);
    public Task<bool> RemoveCreaturesAsync(ulong id, HashSet<ulong> creatureIds);
    public Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds);
    public Task<bool> RemoveCirclesAsync(ulong id, HashSet<ulong> circleIds);
}
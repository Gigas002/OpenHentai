using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public interface ICreatureContextHelper
{
    public Task<IEnumerable<CreaturesNames>?> GetNamesAsync(ulong id);

    public Task<IEnumerable<Tag>?> GetTagsAsync(ulong id);

    public Task<IEnumerable<CreaturesRelations>?> GetRelationsAsync(ulong id);

    public Task<bool> AddNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names);

    public Task<bool> AddRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations);

    public Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds);

    public Task<bool> RemoveNamesAsync(ulong id, HashSet<ulong> nameIds);

    public Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds);

    public Task<bool> RemoveRelationsAsync(ulong id, HashSet<ulong> relatedIds);
}

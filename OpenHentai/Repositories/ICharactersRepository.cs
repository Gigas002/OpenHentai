using OpenHentai.Creatures;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Repositories;

public interface ICharactersRepository : ICreaturesRepository<Character>
{
    public IEnumerable<Character> GetCharacters();
    public Task<IEnumerable<CreationsCharacters>?> GetCreationsAsync(ulong id);
    public Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, CharacterRole> creationRoles);
    public Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds);
}

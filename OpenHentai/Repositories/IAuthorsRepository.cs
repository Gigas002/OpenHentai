using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Repositories;

public interface IAuthorsRepository : ICreaturesRepository<Author>
{
    public IEnumerable<Author> GetAuthors();
    public IEnumerable<AuthorsNames> GetAuthorsNames();
    public Task<IEnumerable<AuthorsNames>?> GetAuthorNamesAsync(ulong id);
    public Task<IEnumerable<Circle>?> GetCirclesAsync(ulong id);
    public Task<IEnumerable<AuthorsCreations>?> GetCreationsAsync(ulong id);
    public Task<bool> AddAuthorNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names);
    public Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds);
    public Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles);
    public Task<bool> RemoveAuthorNamesAsync(ulong id, HashSet<ulong> nameIds);
    public Task<bool> RemoveCirclesAsync(ulong id, HashSet<ulong> circleIds);
    public Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds);
}

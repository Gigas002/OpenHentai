using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Contexts;

public class CharactersContextHelper : CreaturesContextHelper<Character>
{
    #region Constructors

    public CharactersContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Character> GetCharacters() => Context.Characters;

    public async Task<IEnumerable<CreationsCharacters>?> GetCreationsAsync(ulong id)
    {
        var character = await Context.Characters.Include(c => c.Creations)
                                     .ThenInclude(cc => cc.Origin)
                                     .FirstOrDefaultAsync(c => c.Id == id);

        return character?.Creations;
    }

    #endregion

    #region Add

    public async Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, CharacterRole> creationRoles)
    {
        if (creationRoles is null || creationRoles.Count <= 0) return false;

        var character = await GetEntryAsync<Character>(id);

        if (character is null) return false;

        foreach (var creationRole in creationRoles)
        {
            var creation = await GetEntryAsync<Creation>(creationRole.Key);

            if (creation is null) return false;

            character.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var character = await Context.Characters.Include(c => c.Creations)
                                  .ThenInclude(cc => cc.Origin)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (character is null) return false;

        foreach (var creationId in creationIds)
            character.Creations.RemoveWhere(c => c.Related.Id == creationId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

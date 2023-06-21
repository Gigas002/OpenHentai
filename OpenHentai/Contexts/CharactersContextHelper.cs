using Microsoft.EntityFrameworkCore;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public class CharactersContextHelper : DatabaseContextHelper
{
    #region Constructors

    public CharactersContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Character> GetCharacters() => Context.Characters;

    public ValueTask<Character?> GetCharacterAsync(ulong id) => Context.Characters.FindAsync(id);

    public async Task<IEnumerable<CreationsCharacters>?> GetCreationsAsync(ulong id)
    {
        var character = await Context.Characters.Include(c => c.Creations)
                                     .ThenInclude(cc => cc.Origin)
                                     .FirstOrDefaultAsync(c => c.Id == id);

        return character?.Creations;
    }

    public async Task<IEnumerable<CreaturesNames>?> GetNamesAsync(ulong id)
    {
        var character = await Context.Characters.Include(c => c.Names)
                                   .FirstOrDefaultAsync(c => c.Id == id);

        return character?.Names;
    }

    public async Task<IEnumerable<Tag>?> GetTagsAsync(ulong id)
    {
        var character = await Context.Characters.Include(c => c.Tags)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return character?.Tags;
    }

    public async Task<IEnumerable<CreaturesRelations>?> GetRelationsAsync(ulong id)
    {
        var character = await Context.Characters.Include(c => c.Relations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return character?.Relations;
    }

    #endregion

    #region Add

    public async Task<bool> AddCharacterAsync(Character character)
    {
        if (character is null) return false;

        await Context.Characters.AddAsync(character);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var character = await GetCharacterAsync(id);

        if (character is null) return false;

        character.AddNames(names);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations)
    {
        if (relations is null || relations.Count <= 0) return false;

        var character = await GetCharacterAsync(id);

        if (character is null) return false;

        foreach (var relation in relations)
        {
            var related = await Context.Creatures.FindAsync(relation.Key);

            if (related is null) return false;

            character.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, CharacterRole> creationRoles)
    {
        if (creationRoles is null || creationRoles.Count <= 0) return false;

        var character = await GetCharacterAsync(id);

        if (character is null) return false;

        foreach (var creationRole in creationRoles)
        {
            var creation = await Context.Creations.FindAsync(creationRole.Key);

            if (creation is null) return false;

            character.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var character = await GetCharacterAsync(id);

        if (character is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await Context.Tags.FindAsync(tagId);

            if (tag is null) return false;

            character.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveCharacterAsync(ulong id)
    {
        var character = await GetCharacterAsync(id);

        if (character is null) return false;

        Context.Characters.Remove(character);

        await Context.SaveChangesAsync();

        return true;
    }

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

    public async Task<bool> RemoveNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        if (nameIds is null || nameIds.Count <= 0) return false;

        var character = await Context.Characters.Include(c => c.Names)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (character is null) return false;

        foreach (var nameId in nameIds)
            character.Names.RemoveWhere(cn => cn.Id == nameId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var character = await Context.Characters.Include(c => c.Tags)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (character is null) return false;

        foreach (var tagId in tagIds)
            character.Tags.RemoveWhere(t => t.Id == tagId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        if (relatedIds is null || relatedIds.Count <= 0) return false;

        var character = await Context.Characters.Include(c => c.Relations)
                                  .ThenInclude(cr => cr.Related)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (character is null) return false;

        foreach (var relatedId in relatedIds)
            character.Relations.RemoveWhere(cr => cr.Related.Id == relatedId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Experimental

    public static async Task UpdateCharacterAsync(DatabaseContext context, ulong id, Character newCharacter)
    {
        newCharacter.Id = id;

        context.Attach(newCharacter);
        context.Characters.Update(newCharacter);

        await context.SaveChangesAsync();
    }

    #endregion

    #endregion
}

using Microsoft.EntityFrameworkCore;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public abstract class CreaturesContextHelper<T> : DatabaseContextHelper
    where T : Creature
{
    #region Constructors

    protected CreaturesContextHelper(DatabaseContext context) : base(context)
    { }

    #endregion

    #region Methods

    #region Get

    public async Task<IEnumerable<CreaturesNames>?> GetNamesAsync(ulong id)
    {
        var creature = await Context.Creatures.Include(c => c.Names)
                                   .FirstOrDefaultAsync(c => c.Id == id);

        return creature?.Names;
    }

    public async Task<IEnumerable<Tag>?> GetTagsAsync(ulong id)
    {
        var creature = await Context.Creatures.Include(c => c.Tags)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return creature?.Tags;
    }

    public async Task<IEnumerable<CreaturesRelations>?> GetRelationsAsync(ulong id)
    {
        var creature = await Context.Creatures.Include(c => c.Relations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return creature?.Relations;
    }

    #endregion

    #region Add

    public async Task<bool> AddNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var creature = await GetEntryAsync<T>(id);

        if (creature is null) return false;

        creature.AddNames(names);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations)
    {
        if (relations is null || relations.Count <= 0) return false;

        var creature = await GetEntryAsync<T>(id);

        if (creature is null) return false;

        foreach (var relation in relations)
        {
            var related = await GetEntryAsync<Creature>(relation.Key);

            if (related is null) return false;

            creature.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var creature = await GetEntryAsync<T>(id);

        if (creature is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await GetEntryAsync<Tag>(tagId);

            if (tag is null) return false;

            creature.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        if (nameIds is null || nameIds.Count <= 0) return false;

        var creature = await Context.Creatures.Include(c => c.Names)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creature is null) return false;

        foreach (var nameId in nameIds)
            creature.Names.RemoveWhere(cn => cn.Id == nameId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var creature = await Context.Creatures.Include(c => c.Tags)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creature is null) return false;

        foreach (var tagId in tagIds)
            creature.Tags.RemoveWhere(t => t.Id == tagId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        if (relatedIds is null || relatedIds.Count <= 0) return false;

        var creature = await Context.Creatures.Include(c => c.Relations)
                                  .ThenInclude(cr => cr.Related)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creature is null) return false;

        foreach (var relatedId in relatedIds)
            creature.Relations.RemoveWhere(cr => cr.Related.Id == relatedId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

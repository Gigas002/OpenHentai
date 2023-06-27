using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Tags;

namespace OpenHentai.Repositories;

public class TagsRepository : DatabaseRepository, ITagsRepository
{
    #region Constructors

    public TagsRepository(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Tag> GetTags() => Context.Tags;

    public async Task<IEnumerable<Creature>?> GetCreaturesAsync(ulong id)
    {
        var tag = await Context.Tags.Include(t => t.Creatures)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return tag?.Creatures;
    }

    public async Task<IEnumerable<Creation>?> GetCreationsAsync(ulong id)
    {
        var tag = await Context.Tags.Include(t => t.Creations)
                          .FirstOrDefaultAsync(t => t.Id == id);

        return tag?.Creations;
    }

    public async Task<IEnumerable<Circle>?> GetCirclesAsync(ulong id)
    {
        var tag = await Context.Tags.Include(t => t.Circles)
                     .FirstOrDefaultAsync(t => t.Id == id);

        return tag?.Circles;
    }

    #endregion

    #region Add

    public async Task<bool> AddCreaturesAsync(ulong id, HashSet<ulong> creatureIds)
    {
        if (creatureIds is null || creatureIds.Count <= 0) return false;

        var tag = await GetEntryAsync<Tag>(id);

        if (tag is null) return false;

        foreach (var creatureId in creatureIds)
        {
            var creature = await GetEntryAsync<Creature>(creatureId);

            if (creature is null) return false;

            tag.Creatures.Add(creature);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var tag = await GetEntryAsync<Tag>(id);

        if (tag is null) return false;

        foreach (var creationId in creationIds)
        {
            var creation = await GetEntryAsync<Creation>(creationId);

            if (creation is null) return false;

            tag.Creations.Add(creation);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var tag = await GetEntryAsync<Tag>(id);

        if (tag is null) return false;

        foreach (var circleId in circleIds)
        {
            var circle = await GetEntryAsync<Circle>(circleId);

            if (circle is null) return false;

            tag.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveCreaturesAsync(ulong id, HashSet<ulong> creatureIds)
    {
        if (creatureIds is null || creatureIds.Count <= 0) return false;

        var tag = await Context.Tags.Include(t => t.Creatures)
                                  .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return false;

        var removedItems = 0;

        foreach (var creatureId in creatureIds)
            removedItems = tag.Creatures.RemoveWhere(c => c.Id == creatureId);

        if (removedItems <= 0) return false;

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var tag = await Context.Tags.Include(t => t.Creations)
                                  .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return false;

        var removedItems = 0;

        foreach (var creationId in creationIds)
            removedItems = tag.Creations.RemoveWhere(c => c.Id == creationId);

        if (removedItems <= 0) return false;

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var tag = await Context.Tags.Include(t => t.Circles)
                                  .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return false;

        var removedItems = 0;

        foreach (var circleId in circleIds)
            removedItems = tag.Circles.RemoveWhere(c => c.Id == circleId);

        if (removedItems <= 0) return false;

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Repositories;

public class CirclesRepository : DatabaseRepository, ICirclesRepository
{
    #region Constructors

    public CirclesRepository(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Circle> GetCircles() => Context.Circles;

    public IEnumerable<CirclesTitles> GetAllTitles() => Context.CirclesTitles.Include(ct => ct.Entity);

    public async Task<IEnumerable<CirclesTitles>?> GetTitlesAsync(ulong id)
    {
        var circle = await Context.Circles.Include(c => c.Titles)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        return circle?.Titles;
    }

    public async Task<IEnumerable<Author>?> GetAuthorsAsync(ulong id)
    {
        var circle = await Context.Circles.Include(c => c.Authors)
                          .FirstOrDefaultAsync(c => c.Id == id);

        return circle?.Authors;
    }

    public async Task<IEnumerable<Creation>?> GetCreationsAsync(ulong id)
    {
        var circle = await Context.Circles.Include(c => c.Creations)
                     .FirstOrDefaultAsync(c => c.Id == id);

        return circle?.Creations;
    }

    public async Task<IEnumerable<Tag>?> GetTagsAsync(ulong id)
    {
        var circle = await Context.Circles.Include(c => c.Tags)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return circle?.Tags;
    }

    #endregion

    #region Add

    public async Task<bool> AddTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles)
    {
        var circle = await GetEntryAsync<Circle>(id);

        if (circle == null) return false;

        circle.AddTitles(titles);

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddAuthorsAsync(ulong id, HashSet<ulong> authorsIds)
    {
        if (authorsIds is null || authorsIds.Count <= 0) return false;

        var circle = await GetEntryAsync<Circle>(id);

        if (circle is null) return false;

        foreach (var authorId in authorsIds)
        {
            var author = await GetEntryAsync<Author>(authorId);

            if (author is null) return false;

            circle.Authors.Add(author);
        }

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, HashSet<ulong> creationsIds)
    {
        if (creationsIds is null || creationsIds.Count <= 0) return false;

        var circle = await GetEntryAsync<Circle>(id);

        if (circle is null) return false;

        foreach (var creationId in creationsIds)
        {
            var creation = await GetEntryAsync<Creation>(creationId);

            if (creation is null) return false;

            circle.Creations.Add(creation);
        }

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var circle = await GetEntryAsync<Circle>(id);

        if (circle is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await GetEntryAsync<Tag>(tagId);

            if (tag is null) return false;

            circle.Tags.Add(tag);
        }

        await SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveTitlesAsync(ulong id, HashSet<ulong> titleIds)
    {
        if (titleIds is null || titleIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Titles)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        var removedItems = 0;

        foreach (var titleId in titleIds)
            removedItems = circle.Titles.RemoveWhere(ct => ct.Id == titleId);

        if (removedItems <= 0) return false;

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveAuthorsAsync(ulong id, HashSet<ulong> authorIds)
    {
        if (authorIds is null || authorIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Authors)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        var removedItems = 0;

        foreach (var authorId in authorIds)
            removedItems = circle.Authors.RemoveWhere(a => a.Id == authorId);

        if (removedItems <= 0) return false;

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Creations)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        var removedItems = 0;

        foreach (var creationId in creationIds)
            removedItems = circle.Creations.RemoveWhere(c => c.Id == creationId);

        if (removedItems <= 0) return false;

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Tags)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        var removedItems = 0;

        foreach (var tagId in tagIds)
            removedItems = circle.Tags.RemoveWhere(t => t.Id == tagId);

        if (removedItems <= 0) return false;

        await SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

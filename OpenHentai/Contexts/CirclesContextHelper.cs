using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public class CirclesContextHelper : DatabaseContextHelper
{
    #region Constructors

    public CirclesContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Circle> GetCircles() => Context.Circles;

    public ValueTask<Circle?> GetCircleAsync(ulong id) => Context.Circles.FindAsync(id);

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

    public async Task<bool> AddCircleAsync(Circle circle)
    {
        if (circle is null) return false;

        await Context.Circles.AddAsync(circle);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles)
    {
        var circle = await GetCircleAsync(id);

        if (circle == null) return false;

        circle.AddTitles(titles);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddAuthorsAsync(ulong id, HashSet<ulong> authorsIds)
    {
        if (authorsIds is null || authorsIds.Count <= 0) return false;

        var circle = await GetCircleAsync(id);

        if (circle is null) return false;

        foreach (var authorId in authorsIds)
        {
            var author = await Context.Authors.FindAsync(authorId);

            if (author is null) return false;

            circle.Authors.Add(author);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, HashSet<ulong> creationsIds)
    {
        if (creationsIds is null || creationsIds.Count <= 0) return false;

        var circle = await GetCircleAsync(id);

        if (circle is null) return false;

        foreach (var creationId in creationsIds)
        {
            var creation = await Context.Creations.FindAsync(creationId);

            if (creation is null) return false;

            circle.Creations.Add(creation);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var circle = await GetCircleAsync(id);

        if (circle is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await Context.Tags.FindAsync(tagId);

            if (tag is null) return false;

            circle.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveCircleAsync(ulong id)
    {
        var circle = await GetCircleAsync(id);

        if (circle is null) return false;

        Context.Circles.Remove(circle);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTitlesAsync(ulong id, HashSet<ulong> titleIds)
    {
        if (titleIds is null || titleIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Titles)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        foreach (var titleId in titleIds)
            circle.Titles.RemoveWhere(ct => ct.Id == titleId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveAuthorsAsync(ulong id, HashSet<ulong> authorIds)
    {
        if (authorIds is null || authorIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Authors)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        foreach (var authorId in authorIds)
            circle.Authors.RemoveWhere(a => a.Id == authorId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Creations)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        foreach (var creationId in creationIds)
            circle.Creations.RemoveWhere(c => c.Id == creationId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var circle = await Context.Circles.Include(c => c.Tags)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null) return false;

        foreach (var tagId in tagIds)
            circle.Tags.RemoveWhere(t => t.Id == tagId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Experimental

    public static async Task UpdateCircleAsync(DatabaseContext context, ulong id, Circle newCircle)
    {
        newCircle.Id = id;

        context.Attach(newCircle);
        context.Circles.Update(newCircle);

        await context.SaveChangesAsync();
    }

    #endregion

    #endregion
}

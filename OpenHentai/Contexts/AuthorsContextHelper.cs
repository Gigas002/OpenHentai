using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Contexts;

public class AuthorsContextHelper : CreaturesContextHelper<Author>
{
    #region Constructors

    public AuthorsContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Author> GetAuthors() => Context.Authors;

    public IEnumerable<AuthorsNames> GetAuthorsNames() => Context.AuthorsNames.Include(an => an.Entity);

    public async Task<IEnumerable<AuthorsNames>?> GetAuthorNamesAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.AuthorNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return author?.AuthorNames;
    }

    public async Task<IEnumerable<Circle>?> GetCirclesAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.Circles)
                          .FirstOrDefaultAsync(a => a.Id == id);

        return author?.Circles;
    }

    public async Task<IEnumerable<AuthorsCreations>?> GetCreationsAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.Creations)
                     .ThenInclude(ac => ac.Related)
                     .FirstOrDefaultAsync(a => a.Id == id);

        return author?.Creations;
    }

    #endregion

    #region Add

    public async Task<bool> AddAuthorNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var author = await GetEntryAsync<Author>(id);

        if (author == null) return false;

        author.AddAuthorNames(names);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var author = await GetEntryAsync<Author>(id);

        if (author is null) return false;

        foreach (var circleId in circleIds)
        {
            var circle = await GetEntryAsync<Circle>(circleId);

            if (circle is null) return false;

            author.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles)
    {
        if (creationRoles is null || creationRoles.Count <= 0) return false;

        var author = await GetEntryAsync<Author>(id);

        if (author is null) return false;

        foreach (var creationRole in creationRoles)
        {
            var creation = await GetEntryAsync<Creation>(creationRole.Key);

            if (creation is null) return false;

            author.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveAuthorNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        if (nameIds is null || nameIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.AuthorNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var nameId in nameIds)
            author.AuthorNames.RemoveWhere(an => an.Id == nameId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.Circles)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var circleId in circleIds)
            author.Circles.RemoveWhere(c => c.Id == circleId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        if (creationIds is null || creationIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.Creations)
                                  .ThenInclude(ac => ac.Related)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var creationId in creationIds)
            author.Creations.RemoveWhere(c => c.Related.Id == creationId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

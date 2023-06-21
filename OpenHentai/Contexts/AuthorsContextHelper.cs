using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public class AuthorsContextHelper : DatabaseContextHelper
{
    #region Constructors

    public AuthorsContextHelper(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region Get

    public IEnumerable<Author> GetAuthors() => Context.Authors;

    public ValueTask<Author?> GetAuthorAsync(ulong id) => Context.Authors.FindAsync(id);

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

    public async Task<IEnumerable<CreaturesNames>?> GetNamesAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.Names)
                                   .FirstOrDefaultAsync(a => a.Id == id);

        return author?.Names;
    }

    public async Task<IEnumerable<Tag>?> GetTagsAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.Tags)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return author?.Tags;
    }

    public async Task<IEnumerable<CreaturesRelations>?> GetRelationsAsync(ulong id)
    {
        var author = await Context.Authors.Include(a => a.Relations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return author?.Relations;
    }

    #endregion

    #region Add

    public async Task AddAuthorAsync(Author author)
    {
        await Context.Authors.AddAsync(author);

        await Context.SaveChangesAsync();
    }

    public async Task<bool> AddAuthorNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var author = await GetAuthorAsync(id);

        if (author == null) return false;

        author.AddAuthorNames(names);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        author.AddNames(names);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations)
    {
        if (relations is null || relations.Count <= 0) return false;

        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        foreach (var relation in relations)
        {
            var related = await Context.Creatures.FindAsync(relation.Key);

            if (related is null) return false;

            author.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        foreach (var circleId in circleIds)
        {
            var circle = await Context.Circles.FindAsync(circleId);

            if (circle is null) return false;

            author.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles)
    {
        if (creationRoles is null || creationRoles.Count <= 0) return false;

        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        foreach (var creationRole in creationRoles)
        {
            var creation = await Context.Creations.FindAsync(creationRole.Key);

            if (creation is null) return false;

            author.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await Context.Tags.FindAsync(tagId);

            if (tag is null) return false;

            author.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveAuthorAsync(ulong id)
    {
        var author = await GetAuthorAsync(id);

        if (author is null) return false;

        Context.Authors.Remove(author);

        await Context.SaveChangesAsync();

        return true;
    }

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

    public async Task<bool> RemoveNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        if (nameIds is null || nameIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.Names)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var nameId in nameIds)
            author.Names.RemoveWhere(cn => cn.Id == nameId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.Tags)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var tagId in tagIds)
            author.Tags.RemoveWhere(t => t.Id == tagId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        if (relatedIds is null || relatedIds.Count <= 0) return false;

        var author = await Context.Authors.Include(a => a.Relations)
                                  .ThenInclude(cr => cr.Related)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        if (author is null) return false;

        foreach (var relatedId in relatedIds)
            author.Relations.RemoveWhere(cr => cr.Related.Id == relatedId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Experimental

    public static async Task UpdateAuthorAsync(DatabaseContext context, ulong id, Author newAuthor)
    {
        newAuthor.Id = id;

        context.Attach(newAuthor);
        context.Authors.Update(newAuthor);

        await context.SaveChangesAsync();
    }

    #endregion

    #endregion
}

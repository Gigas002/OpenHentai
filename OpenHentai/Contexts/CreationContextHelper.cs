using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.Contexts;

public abstract class CreationContextHelper<T> : DatabaseContextHelper
    where T : Creation
{
    #region Constructors

    protected CreationContextHelper(DatabaseContext context) : base(context)
    { }

    #endregion

    #region Methods

    #region Get

    public async Task<IEnumerable<CreationsTitles>?> GetTitlesAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Titles)
                                 .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Titles;
    }

    public async Task<IEnumerable<AuthorsCreations>?> GetAuthorsAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Authors)
                            .ThenInclude(ac => ac.Origin)
                          .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Authors;
    }

    public async Task<IEnumerable<Circle>?> GetCirclesAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Circles)
                     .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Circles;
    }

    public async Task<IEnumerable<CreationsRelations>?> GetRelationsAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Relations)
                                 .ThenInclude(cr => cr.Related)
                                 .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Relations;
    }

    public async Task<IEnumerable<CreationsCharacters>?> GetCharactersAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Characters)
                                    .ThenInclude(cc => cc.Related)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Characters;
    }

    public async Task<IEnumerable<Tag>?> GetTagsAsync(ulong id)
    {
        var creation = await Context.Creations.Include(c => c.Tags)
                                    .FirstOrDefaultAsync(c => c.Id == id);

        return creation?.Tags;
    }

    #endregion

    #region Add

    public async Task<bool> AddTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles)
    {
        var creation = await GetEntryAsync<T>(id);

        if (creation == null) return false;

        creation.AddTitles(titles);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddAuthorsAsync(ulong id, Dictionary<ulong, AuthorRole> authorsRoles)
    {
        if (authorsRoles is null || authorsRoles.Count <= 0) return false;

        var creation = await GetEntryAsync<T>(id);

        if (creation is null) return false;

        foreach (var authorRole in authorsRoles)
        {
            var author = await GetEntryAsync<Author>(authorRole.Key);

            if (author is null) return false;

            creation.AddAuthor(author, authorRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var creation = await GetEntryAsync<T>(id);

        if (creation is null) return false;

        foreach (var circleId in circleIds)
        {
            var circle = await GetEntryAsync<Circle>(circleId);

            if (circle is null) return false;

            creation.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddRelationsAsync(ulong id, Dictionary<ulong, CreationRelations> relations)
    {
        if (relations is null || relations.Count <= 0) return false;

        var creation = await GetEntryAsync<T>(id);

        if (creation is null) return false;

        foreach (var relation in relations)
        {
            var related = await GetEntryAsync<Creation>(relation.Key);

            if (related is null) return false;

            creation.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCharactersAsync(ulong id, Dictionary<ulong, CharacterRole> charactersRoles)
    {
        if (charactersRoles is null || charactersRoles.Count <= 0) return false;

        var creation = await GetEntryAsync<T>(id);

        if (creation is null) return false;

        foreach (var characterRole in charactersRoles)
        {
            var character = await GetEntryAsync<Character>(characterRole.Key);

            if (character is null) return false;

            creation.AddCharacter(character, characterRole.Value);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var creation = await GetEntryAsync<T>(id);

        if (creation is null) return false;

        foreach (var tagId in tagIds)
        {
            var tag = await GetEntryAsync<Tag>(tagId);

            if (tag is null) return false;

            creation.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Remove

    public async Task<bool> RemoveTitlesAsync(ulong id, HashSet<ulong> titleIds)
    {
        if (titleIds is null || titleIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Titles)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var titleId in titleIds)
            creation.Titles.RemoveWhere(ct => ct.Id == titleId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveAuthorsAsync(ulong id, HashSet<ulong> authorIds)
    {
        if (authorIds is null || authorIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Authors)
                                    .ThenInclude(ac => ac.Origin)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var authorId in authorIds)
            creation.Authors.RemoveWhere(a => a.Origin.Id == authorId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        if (circleIds is null || circleIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Circles)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var circleId in circleIds)
            creation.Circles.RemoveWhere(c => c.Id == circleId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        if (relatedIds is null || relatedIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Relations)
                                    .ThenInclude(cr => cr.Related)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var relatedId in relatedIds)
            creation.Relations.RemoveWhere(cr => cr.Related.Id == relatedId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCharactersAsync(ulong id, HashSet<ulong> characterIds)
    {
        if (characterIds is null || characterIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Characters)
                                    .ThenInclude(ac => ac.Related)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var characterId in characterIds)
            creation.Characters.RemoveWhere(a => a.Related.Id == characterId);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        if (tagIds is null || tagIds.Count <= 0) return false;

        var creation = await Context.Creations.Include(c => c.Tags)
                                  .FirstOrDefaultAsync(c => c.Id == id);

        if (creation is null) return false;

        foreach (var tagId in tagIds)
            creation.Tags.RemoveWhere(t => t.Id == tagId);

        await Context.SaveChangesAsync();

        return true;
    }

    #endregion

    #endregion
}

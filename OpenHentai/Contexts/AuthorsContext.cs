using Microsoft.EntityFrameworkCore;
using OpenHentai.Creatures;

namespace OpenHentai.Contexts;

public static class AuthorsContext
{
    public static DbSet<Author> GetAuthors(DatabaseContext context) => context.Authors;

    public static IEnumerable<Author> GetAuthorsWithProps(DatabaseContext context) => 
        context.Authors.Include(a => a.AuthorsNames)
            .Include(a => a.Circles)
            .Include(a => a.AuthorsCreations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.CreaturesNames)
            .Include(a => a.Tags)
            .Include(a => a.CreaturesRelations)
            .ThenInclude(cr => cr.Related);

    public static ValueTask<Author?> GetAuthorAsync(DatabaseContext context, ulong id) =>
        context.Authors.FindAsync(id);

    public static Task<Author?> GetAuthorWithPropsAsync(DatabaseContext context, ulong id)
    {
        var authors = context.Authors
            .Include(a => a.AuthorsNames)
            .Include(a => a.Circles)
            .Include(a => a.AuthorsCreations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.CreaturesNames)
            .Include(a => a.Tags)
            .Include(a => a.CreaturesRelations)
            .ThenInclude(cr => cr.Related);

        return authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public static async Task AddAuthorAsync(DatabaseContext context, Author author, bool saveChanges = true)
    {
        await context.Authors.AddAsync(author);

        if (saveChanges) await context.SaveChangesAsync();
    }

    public static async Task DeleteAuthorAsync(DatabaseContext context, Author author, bool saveChanges = true)
    {
        context.Authors.Remove(author);

        if (saveChanges) await context.SaveChangesAsync();
    }

    public static async Task<Author> DeleteAuthorAsync(DatabaseContext context, ulong id, bool saveChanges = true)
    {
        var author = await context.Authors.FindAsync(id);

        await DeleteAuthorAsync(context, author, saveChanges);

        return author;
    }

    public static async Task UpdateAuthorAsync(DatabaseContext context, ulong id, Author newAuthor)
    {
        newAuthor.Id = id;

        context.Attach(newAuthor);
        context.Authors.Update(newAuthor);
        
        await context.SaveChangesAsync();
    }
}

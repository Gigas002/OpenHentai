using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using SystemTextJsonPatch;
using OpenHentai.Relative;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Tags;
using SystemTextJsonPatch.Operations;
using OpenHentai.Roles;
using OpenHentai.Relations;
using OpenHentai.Descriptors;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/authors")]
public class AuthorController : DatabaseController, ICreatureController
{
    #region Constructors

    /// <inheritdoc/>
    public AuthorController(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region GET

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Author>> GetAuthors()
    {
        Console.WriteLine($"Enter into GET: /authors");

        var authors = Context.Authors;

        return Ok(authors);
    }

    // GET: authors/5
    /// <summary>
    /// Get author from database by id
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Author</returns>
    /// <response code="200">Returns requested author</response>
    /// <response code="400">Author is null</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> GetAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}");

        var author = await AuthorsContext.GetAuthorAsync(Context, id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }

    [HttpGet("authors_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsNames>> GetAuthorsNames()
    {
        Console.WriteLine($"Enter into GET: /authors/authors_names");

        var names = Context.AuthorsNames.Include(an => an.Entity).ToList();

        return names;
    }

    [HttpGet("{id}/author_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsNames>>> GetAuthorNamesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/author_names");

        var author = await Context.Authors.Include(a => a.AuthorNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return author.AuthorNames;
    }

    [HttpGet("{id}/circles")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetCirclesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/circles");

        var author = await Context.Authors.Include(a => a.Circles)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return author.Circles;
    }

    [HttpGet("{id}/creations")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsCreations>>> GetCreationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/creations");

        var author = await Context.Authors.Include(a => a.Creations)
                             .ThenInclude(ac => ac.Related)
                             .FirstOrDefaultAsync(a => a.Id == id);

        return author.Creations;
    }

    [HttpGet("{id}/names")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesNames>>> GetNamesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/names");

        var author = await Context.Authors.Include(a => a.Names)
                                   .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.Names);
    }

    [HttpGet("{id}/tags")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/tags");

        var author = await Context.Authors.Include(a => a.Tags)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.Tags);
    }

    [HttpGet("{id}/relations")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/relations");

        var author = await Context.Authors.Include(a => a.Relations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.Relations);
    }

    #endregion

    #region POST

    // POST: authors/
    /// <summary>
    /// Add author to database
    /// </summary>
    /// <param name="author">Author to add</param>
    /// <returns>Created author</returns>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /authors
    ///     { }
    ///
    /// </remarks>
    /// <response code="200">Returns the created author</response>
    /// <response code="400">Author is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> PostAuthorAsync(Author author)
    {
        Console.WriteLine("Enter into POST: /authors");

        if (author == null) return BadRequest();

        await AuthorsContext.AddAuthorAsync(Context, author).ConfigureAwait(false);

        return Ok(author);
    }

    /// <summary>
    /// Updates Author with NEW names, POSTed at their own table
    /// </summary>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/author_names
    ///     [{
    ///         "text": "taras panis",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    [HttpPost("{id}/author_names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostAuthorNamesAsync(ulong id, IEnumerable<LanguageSpecificTextInfo> names)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/author_names");

        var author = await Context.Authors.Include(a => a.AuthorNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        author.AuthorNames.Clear();

        author.AddAuthorNames(names);

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id}/circles")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostCirclesAsync(ulong id, IEnumerable<ulong> circleIds)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/circles");

        var author = await Context.Authors.Include(a => a.Circles)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        author.Circles.Clear();

        foreach (var circleId in circleIds)
        {
            var circle = await Context.Circles.FindAsync(circleId);

            author.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id}/creations")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/creations");

        var author = await Context.Authors.Include(a => a.Creations)
                                          .FirstOrDefaultAsync(a => a.Id == id);

        author.Creations.Clear();

        foreach (var creationRole in creationRoles)
        {
            var creation = await Context.Creations.FindAsync(creationRole.Key);

            author.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/names
    ///     [{
    ///         "text": "Test Minato",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    [HttpPost("{id}/names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostNamesAsync(ulong id, IEnumerable<LanguageSpecificTextInfo> names)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/names");

        var author = await Context.Authors.Include(a => a.Names)
                           .FirstOrDefaultAsync(a => a.Id == id);

        author.Names.Clear();

        author.AddNames(names);

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id}/tags")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostTagsAsync(ulong id, IEnumerable<ulong> tagIds)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/tags");

        var author = await Context.Authors.Include(a => a.Tags)
                           .FirstOrDefaultAsync(a => a.Id == id);

        author.Tags.Clear();

        foreach (var tagId in tagIds)
        {
            var tag = await Context.Tags.FindAsync(tagId);

            author.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id}/relations")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostRelationsAsync(ulong id,
        Dictionary<ulong, CreatureRelations> relations)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/relations");

        var author = await Context.Authors.Include(a => a.Relations)
                                          .FirstOrDefaultAsync(a => a.Id == id);

        author.Relations.Clear();

        foreach (var relation in relations)
        {
            var related = await Context.Creatures.FindAsync(relation.Key);

            author.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region PUT

    // update author entry with EXISTING (posted) names, found by ids
    // since authors_names has author_id defined as ulong - it overrides it's value,
    // removing the name from previously specified entry
    [HttpPut("{id}/author_names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutAuthorNamesAsync(ulong id, IEnumerable<ulong> nameIds)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/author_names");

        var author = await Context.Authors.FindAsync(id);

        foreach (var nameId in nameIds)
        {
            // search through db instead of creating new object is required here
            var name = await Context.AuthorsNames.FindAsync(nameId);

            author.AuthorNames.Add(name);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}/circles")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCirclesAsync(ulong id, IEnumerable<ulong> circleIds)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/circles");

        var author = await Context.Authors.FindAsync(id);

        foreach (var circleId in circleIds)
        {
            var circle = await Context.Circles.FindAsync(circleId);

            author.Circles.Add(circle);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /authors/{id}/creations
    ///     {
    ///         "4": 3,
    ///         "1": 2
    ///     }
    ///
    /// </remarks>
    [HttpPut("{id}/creations")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/creations");

        var author = await Context.Authors.FindAsync(id);

        foreach (var creationRole in creationRoles)
        {
            var creation = await Context.Creations.FindAsync(creationRole.Key);

            author.AddCreation(creation, creationRole.Value);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}/names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutNamesAsync(ulong id, IEnumerable<ulong> nameIds)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/names");

        var author = await Context.Authors.FindAsync(id);

        foreach (var nameId in nameIds)
        {
            // search through db instead of creating new object is required here
            var name = await Context.CreaturesNames.FindAsync(nameId);

            author.Names.Add(name);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}/tags")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutTagsAsync(ulong id, IEnumerable<ulong> tagIds)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/tags");

        var author = await Context.Authors.FindAsync(id);

        foreach (var tagId in tagIds)
        {
            var tag = await Context.Tags.FindAsync(tagId);

            author.Tags.Add(tag);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}/relations")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutRelationsAsync(ulong id,
        Dictionary<ulong, CreatureRelations> relations)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/relations");

        var author = await Context.Authors.FindAsync(id);

        foreach (var relation in relations)
        {
            var related = await Context.Creatures.FindAsync(relation.Key);

            author.AddRelation(related, relation.Value);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}");

        var author = await AuthorsContext.DeleteAuthorAsync(Context, id).ConfigureAwait(false);

        return Ok(author);
    }

    [HttpDelete("{id}/author_names")]
    public async Task<ActionResult> DeleteAuthorNamesAsync(ulong id, IEnumerable<ulong> nameIds)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}/author_names");

        var author = await Context.Authors.Include(a => a.AuthorNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        foreach (var nameId in nameIds)
            author.AuthorNames.RemoveWhere(an => an.Id == nameId);

        await Context.SaveChangesAsync();

        return Ok(author);
    }

    [HttpDelete("{id}/circles")]
    public async Task<ActionResult> DeleteCirclesAsync(ulong id, IEnumerable<ulong> circleIds)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}");

        var author = await Context.Authors.Include(a => a.Circles)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        foreach (var circleId in circleIds)
            author.Circles.RemoveWhere(c => c.Id == circleId);

        await Context.SaveChangesAsync();

        return Ok(author);
    }

    [HttpDelete("{id}/creations")]
    public async Task<ActionResult> DeleteCreationsAsync(ulong id, IEnumerable<ulong> creationIds)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}/creations");

        var author = await Context.Authors.Include(a => a.Creations)
                                  .ThenInclude(ac => ac.Related)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        foreach (var creationId in creationIds)
            author.Creations.RemoveWhere(c => c.Related.Id == creationId);

        await Context.SaveChangesAsync();

        return Ok(author);
    }

    #endregion

    #region PATCH

    // TODO: requires more testing and improvements

    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /authors/{id}
    ///     [{
    ///         "path": "/age",
    ///         "op": "replace",
    ///         "value": 30
    ///     },
    ///     {
    ///         "path": "/authorsNames",
    ///         "op": "add",
    ///         "value": [{
    ///           "author_id": 8,
    ///           "name": "Test Bubato",
    ///           "language": null
    ///         }]
    ///     }]
    ///
    /// </remarks>
    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<Author>> PatchAuthorAsync(ulong id,
        IEnumerable<Operation<Author>> operations)
    {
        Console.WriteLine($"Enter into PATCH: /authors/{id}");

        var patch = new JsonPatchDocument<Author>(operations.ToList(), new());

        var user = await Context.Authors.FindAsync(id);

        patch.ApplyTo(user);

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #endregion
}

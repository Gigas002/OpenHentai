using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using SystemTextJsonPatch;
using OpenHentai.Repositories;

namespace OpenHentai.WebAPI.Controllers;

public abstract class DatabaseController<T> : ControllerBase, IDisposable, IAsyncDisposable
    where T : IDatabaseRepository, IDisposable, IAsyncDisposable
{
    #region Properties/fields

    protected bool IsDisposed { get; set; }

    public T Repository { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize database context
    /// </summary>
    protected DatabaseController(T repository) => Repository = repository;

    ~DatabaseController() => Dispose(false);

    #endregion

    #region Methods

    protected async Task<ActionResult<TEntry>> GetEntryAsync<TEntry>(ulong id) where TEntry : class, IDatabaseEntity
    {
        var entry = await Repository.GetEntryAsync<TEntry>(id).ConfigureAwait(false);

        return entry is null ? NotFound() : Ok(entry);
    }

    protected async Task<bool> PostEntryAsync<TEntry>(TEntry entry) where TEntry : class, IDatabaseEntity
    {
        if (entry is null) throw new ArgumentNullException(nameof(entry));

        var isSuccess = await Repository.AddEntryAsync(entry).ConfigureAwait(false);

        return isSuccess;
    }

    protected async Task<ActionResult> DeleteEntryAsync<TEntry>(ulong id) where TEntry : class, IDatabaseEntity
    {
        var isSuccess = await Repository.RemoveEntryAsync<TEntry>(id).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    protected async Task<ActionResult> PatchEntryAsync<TEntry>(ulong id,
        IEnumerable<Operation<TEntry>> operations) where TEntry : class, IDatabaseEntity
    {
        var patch = new JsonPatchDocument<TEntry>(operations.ToList(), Essential.JsonSerializerOptions);

        var entry = await Repository.GetEntryAsync<TEntry>(id).ConfigureAwait(false);

        if (entry is null) return BadRequest();

        try
        {
            patch.ApplyTo(entry);

            await Repository.SaveChangesAsync().ConfigureAwait(false);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }

    #region Dispose

    [ApiExplorerSettings(IgnoreApi = true)]
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed) return;

        if (disposing)
        { }

        Repository.Dispose();

        IsDisposed = true;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(false);
        GC.SuppressFinalize(this);
    }

#pragma warning disable CS1998
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (IsDisposed) return;

        IsDisposed = true;
    }
#pragma warning restore CS1998

    #endregion

    #endregion
}

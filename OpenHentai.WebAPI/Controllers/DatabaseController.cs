using Microsoft.AspNetCore.Mvc;
using OpenHentai.Contexts;

namespace OpenHentai.WebAPI.Controllers;

public abstract class DatabaseController<T> : ControllerBase, IDisposable, IAsyncDisposable
    where T : DatabaseContextHelper, IDisposable, IAsyncDisposable
{
    #region Properties/fields

    protected bool IsDisposed { get; set; }

    public T ContextHelper { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize database context
    /// </summary>
    protected DatabaseController(T contextHelper) => ContextHelper = contextHelper;

    ~DatabaseController() => Dispose(false);

    #endregion

    #region Methods

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

        ContextHelper.Dispose();

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
}

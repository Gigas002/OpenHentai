namespace OpenHentai.Contexts;

public abstract class DatabaseContextHelper : IDisposable, IAsyncDisposable
{
    #region Properties

    protected bool IsDisposed { get; set; }

    public DatabaseContext Context { get; init; }

    #endregion

    #region Constructors

    protected DatabaseContextHelper(DatabaseContext context) => Context = context;

    ~DatabaseContextHelper() => Dispose(false);

    #endregion

    #region Methods

    public ValueTask<T?> GetEntryAsync<T>(ulong id) where T : class, IDatabaseEntity
    {
        return Context.FindAsync<T>(id);
    }

    public async ValueTask<bool> AddEntryAsync<T>(T entry) where T : class, IDatabaseEntity
    {
        if (entry is null) return false;

        await Context.AddAsync(entry);

        await Context.SaveChangesAsync();

        return true;
    }

    public void RemoveEntry<T>(T entry) where T : class, IDatabaseEntity =>
        Context.Remove(entry);

    public async Task<bool> RemoveEntryAsync<T>(ulong id) where T : class, IDatabaseEntity
    {
        var entry = await GetEntryAsync<T>(id);

        if (entry is null) return false;

        RemoveEntry(entry);

        await Context.SaveChangesAsync();

        return true;
    }

    #region Experimental

    public async Task UpdateEntryAsync<T>(ulong id, T entry) where T : class, IDatabaseEntity
    {
        entry.Id = id;

        Context.Attach(entry);
        Context.Update(entry);

        await Context.SaveChangesAsync();
    }

    #endregion

    #region Dispose

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

        Context.Dispose();

        IsDisposed = true;
    }

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

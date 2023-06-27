namespace OpenHentai.Repositories;

public interface IDatabaseRepository : IDisposable, IAsyncDisposable
{
    public DatabaseContext Context { get; init; }
    public ValueTask<T?> GetEntryAsync<T>(ulong id) where T : class, IDatabaseEntity;
    public ValueTask<bool> AddEntryAsync<T>(T entry) where T : class, IDatabaseEntity;
    public void RemoveEntry<T>(T entry) where T : class, IDatabaseEntity;
    public Task<bool> RemoveEntryAsync<T>(ulong id) where T : class, IDatabaseEntity;
    public Task UpdateEntryAsync<T>(ulong id, T entry) where T : class, IDatabaseEntity;
}

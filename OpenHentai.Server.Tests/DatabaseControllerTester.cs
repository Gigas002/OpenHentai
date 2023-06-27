using System.Net;

namespace OpenHentai.WebAPI.Tests;

public abstract class DatabaseControllerTester : IDisposable
{
    public const string IPv6ServerAddress = "https://[::1]:5230";

    public const string IPv4ServerAddress = "https://localhost:5230";

    public static string ServerAddress => IPv4ServerAddress;

    protected bool IsDisposed { get; set; }

    protected HttpClient HttpClient { get; } = new()
    {
        DefaultRequestVersion = HttpVersion.Version30,
        DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
    };

    ~DatabaseControllerTester() => Dispose(false);

    public abstract void Setup();

    public static bool CheckResponse(HttpResponseMessage response) =>
        response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created;

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

        HttpClient.Dispose();

        IsDisposed = true;
    }
}

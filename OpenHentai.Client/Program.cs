using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using OpenHentai.Creatures;

#pragma warning disable CA1303

namespace OpenHentai.Client;

public static class Program
{
    public const string IPv6ServerAddress = "https://[::1]:5230";

    public const string IPv4ServerAddress = "https://localhost:5230";

    static async Task Main()
    {
        var serverAddress = IPv4ServerAddress;
        var authorId = 1;

        using var httpClient = new HttpClient
        {
            DefaultRequestVersion = HttpVersion.Version30,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
        };

        #region GET

        Console.WriteLine("GET");

        var uri = new Uri($"{serverAddress}/authors/{authorId}");

        Stopwatch stopwatch = Stopwatch.StartNew();

        await GetAsync(httpClient, uri).ConfigureAwait(false);

        stopwatch.Stop();

        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

        #endregion
    }

    public static async Task GetAsync(HttpClient httpClient, Uri uri)
    {
        if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

        using var response = await httpClient.GetAsync(uri).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Console.WriteLine(responseText);
        }
        else
        {
            var author = await response.Content.ReadFromJsonAsync<Author>().ConfigureAwait(false);
        }
    }
}

#pragma warning restore CA1303

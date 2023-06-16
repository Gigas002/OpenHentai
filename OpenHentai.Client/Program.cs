using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
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
        ulong authorId = 1;

        using var httpClient = new HttpClient
        {
            DefaultRequestVersion = HttpVersion.Version30,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
        };

        #region GET

        Console.WriteLine("GET");

        var uri = new Uri($"{serverAddress}/authors/{authorId}");

        Stopwatch stopwatch = Stopwatch.StartNew();

        var author = await GetAsync(httpClient, uri).ConfigureAwait(false);

        stopwatch.Stop();

        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

        #endregion

        #region PUT

        // The point of PUT/PATCH is to update existing entry, so we don't need
        // to create a new one, it's supposed to work with the one we GET
        // but still is a good feature to have the ability to push
        // a completely new object

        Console.WriteLine("PUT");

        uri = new Uri($"{serverAddress}/authors/{authorId}");

        author.Age = 2022;

        var newAuthor = new Author("en-US::Maria magdalena") { Age = 2018 };

        using var response = await httpClient.PutAsJsonAsync(uri, newAuthor).ConfigureAwait(false);

        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        #endregion
    }

    public static async Task<Author?> GetAsync(HttpClient httpClient, Uri uri)
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
            return await response.Content.ReadFromJsonAsync<Author>().ConfigureAwait(false);
        }

        return null;
    }
}

#pragma warning restore CA1303

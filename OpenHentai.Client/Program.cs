using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Contexts;
using OpenHentai.Creatures;
using OpenHentai.JsonConverters;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Tags;
using SystemTextJsonPatch;
using SystemTextJsonPatch.Operations;

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

        var stopwatch = Stopwatch.StartNew();

        var author = await GetAsync(httpClient, uri).ConfigureAwait(false);

        stopwatch.Stop();

        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

        #endregion

        #region POST



        #endregion

        #region PATCH

        Console.WriteLine("PATCH");
        
        uri = new Uri($"{serverAddress}/authors/{authorId}");
        
        stopwatch.Restart();
        
        await PatchAsync(httpClient, uri).ConfigureAwait(false);
        
        stopwatch.Stop();
        
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

        #endregion

        #region PUT

        // The point of PUT/PATCH is to update existing entry, so we don't need
        // to create a new one, it's supposed to work with the one we GET
        // but still is a good feature to have the ability to push
        // a completely new object

        // Console.WriteLine("PUT");
        //
        // uri = new Uri($"{serverAddress}/authors/{authorId}");
        //
        // stopwatch.Restart();
        //
        // var changedAuthor = ChangeAuthor(author);
        // var options = Essential.JsonSerializerOptions;
        // var json = JsonSerializer.Serialize(changedAuthor, options);
        // var jsonPath = $"aut.json";
        // File.WriteAllText(jsonPath, json);
        //
        // var putAuthor = await PutAsync(httpClient, uri, changedAuthor).ConfigureAwait(false);
        //
        // stopwatch.Stop();
        //
        // Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

        #endregion
    }

    public static Author ChangeAuthor(Author? author = null)
    {
        var newAuthor = new Author("en-US::Maria magdalena") { Age = 2018 };

        if (author is not null)
        {
            newAuthor = author;
            newAuthor.Age = 2018;
            newAuthor.AddAuthorName("en-US::Maria magdalena");
        }

        return newAuthor;
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

    public static async Task<Author?> PutAsync(HttpClient httpClient, Uri uri, Author author)
    {
        if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

        var opts = new JsonSerializerOptions();
        opts.Converters.Add(new DatabaseEntityCollectionJsonConverter<Tag>());
        opts.Converters.Add(new DatabaseEntityJsonConverter<Tag>());

        using var response = await httpClient.PutAsJsonAsync(uri, author, opts).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.BadRequest)
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

    public static async Task PatchAsync(HttpClient httpClient, Uri uri)
    {
        ulong authorId = 1;

        if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));

        var authorNames = new List<AuthorsNames>();
        authorNames.Add(new(new(authorId), "petrenko", "ru-RU"));
        authorNames.Add(new(new(authorId), "ivan", "ru-RU"));

        var circles = new List<Circle>();
        circles.Add(new Circle(2));

        var creations = new List<AuthorsCreations>();
        // TODO: should not fail if Creations is GET from db, not created new
        creations.Add(new(new(authorId), new(3), AuthorRole.SecondaryArtist));

        var operations = new List<Operation<Author>>
        {
            new Operation<Author>("replace", "/age", null, 444),
            new Operation<Author>("add", "/authornames", null, authorNames),
            new Operation<Author>("add", "/circles", null, circles),
            new Operation<Author>("add", "/creations", null, creations),
        };

        var patchJson = JsonSerializer.Serialize(operations, options: Essential.JsonSerializerOptions);
        using var content = new StringContent(patchJson, Encoding.UTF8, "application/json-patch+json");

        using var response = await httpClient.PatchAsync(uri, content).ConfigureAwait(false);
    }
}

#pragma warning restore CA1303

using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using SystemTextJsonPatch.Operations;

namespace OpenHentai.WebAPI.Tests;

public sealed class AuthorsControllerTests : DatabaseControllerTester
{
    [SetUp]
    public override void Setup()
    {
        // DatabaseInitializer.InitializeTestDatabase();
    }

    #region GET

    [Test]
    [Order(1)]
    public async Task GetAuthorsTest()
    {
        var uri = new Uri($"{ServerAddress}/authors");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetAuthorTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetAuthorsNamesTest()
    {
        var uri = new Uri($"{ServerAddress}/authors/authors_names");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetAuthorNamesTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/author_names");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetCirclesTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/circles");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetCreationsTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/creations");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetNamesTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/names");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetTagsTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/tags");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(1)]
    public async Task GetRelationsTest()
    {
        ulong id = 1;
        var uri = new Uri($"{ServerAddress}/authors/{id}/relations");

        using var response = await HttpClient.GetAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region POST/PUT

    [Test]
    [Order(1)]
    public async Task PostAuthorTest()
    {
        var uri = new Uri($"{ServerAddress}/authors");

        var author = new Author();

        using var response = await HttpClient.PostAsJsonAsync(uri, author).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    // depends on PostAuthorTest
    [Test]
    [Order(2)]
    public async Task PostAuthorNamesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/author_names");

        var authorNames = new HashSet<LanguageSpecificTextInfo>()
        {
            new LanguageSpecificTextInfo("author panis", "ru-RU"),
            new LanguageSpecificTextInfo("author boris", "en-US")
        };

        using var response = await HttpClient.PostAsJsonAsync(uri, authorNames).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(2)]
    public async Task PostNamesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/names");

        var names = new HashSet<LanguageSpecificTextInfo>()
        {
            new LanguageSpecificTextInfo("creature panis", "ru-RU"),
            new LanguageSpecificTextInfo("creature boris", "en-US")
        };

        using var response = await HttpClient.PostAsJsonAsync(uri, names).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(2)]
    public async Task PostRelationsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/relations");

        var relations = new Dictionary<ulong, CreatureRelations>()
        {
            {1, CreatureRelations.Unknown},
            {2, CreatureRelations.Friend}
        };

        using var response = await HttpClient.PostAsJsonAsync(uri, relations).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(2)]
    public async Task PutCirclesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/circles");

        var circleIds = new HashSet<ulong>()
        {
            1, 2
        };

        using var response = await HttpClient.PutAsJsonAsync(uri, circleIds).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(2)]
    public async Task PutCreationsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/creations");

        var creationRoles = new Dictionary<ulong, AuthorRole>()
        {
            {1, AuthorRole.MainArtist},
            {2, AuthorRole.SecondaryArtist}
        };

        using var response = await HttpClient.PutAsJsonAsync(uri, creationRoles).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(2)]
    public async Task PutTagsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/tags");

        var tagIds = new HashSet<ulong>()
        {
            1, 2
        };

        using var response = await HttpClient.PutAsJsonAsync(uri, tagIds).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region PATCH

    [Test]
    [Order(2)]
    public async Task PatchAuthorTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}");

        var operations = new List<Operation<Author>>
        {
            new Operation<Author>("replace", "/age", null, 444)
        };

        var patchJson = JsonSerializer.Serialize(operations, options: Essential.JsonSerializerOptions);
        using var content = new StringContent(patchJson, Encoding.UTF8, "application/json-patch+json");

        using var response = await HttpClient.PatchAsync(uri, content).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    #endregion

    #region DELETE

    [Test]
    [Order(3)]
    public async Task DeleteAuthorNamesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/author_names");

        var nameIds = new HashSet<ulong>()
        {
            5, 6
        };

        var content = JsonSerializer.Serialize(nameIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
    
        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(3)]
    public async Task DeleteCirclesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/circles");

        var circleIds = new HashSet<ulong>()
        {
            1, 2
        };

        var content = JsonSerializer.Serialize(circleIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(3)]
    public async Task DeleteCreationsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/creations");

        var creationIds = new HashSet<ulong>()
        {
            1, 2
        };

        var content = JsonSerializer.Serialize(creationIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(3)]
    public async Task DeleteNamesTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/names");

        var nameIds = new HashSet<ulong>()
        {
            7, 8
        };

        var content = JsonSerializer.Serialize(nameIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(3)]
    public async Task DeleteTagsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/tags");

        var tagIds = new HashSet<ulong>()
        {
            1, 2
        };

        var content = JsonSerializer.Serialize(tagIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(3)]
    public async Task DeleteRelationsTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}/relations");

        var relationIds = new HashSet<ulong>()
        {
            1, 2
        };

        var content = JsonSerializer.Serialize(relationIds, options: new());

        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    [Test]
    [Order(4)]
    public async Task DeleteAuthorTest()
    {
        ulong id = 7;
        var uri = new Uri($"{ServerAddress}/authors/{id}");

        using var response = await HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        if (!CheckResponse(response)) Assert.Fail();
    }

    #endregion
}

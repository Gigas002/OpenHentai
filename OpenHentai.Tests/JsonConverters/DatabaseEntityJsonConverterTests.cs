using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Relative;

namespace OpenHentai.Tests.JsonConverters;

public class DatabaseEntityJsonConverterTests
{
    private const string Json = "[{\"author_id\":1,\"name\":\"name\",\"language\":\"default\"}]";

    [Test]
    public void SerializationTest()
    {
        const ulong id = 1;
        var authorMock = new Mock<Author>(id);
        authorMock.Object.AddAuthorName("default::name");

        var ser = JsonSerializer.Serialize(authorMock.Object.AuthorNames, Essential.JsonSerializerOptions);

        if (!ser.Equals(Json, StringComparison.Ordinal))
            Assert.Fail();
    }

    [Test]
    public void DeserializationTest()
    {
        const ulong id = 1;
        var authorMock = new Mock<Author>(id);
        authorMock.Object.AddAuthorName("default::name");

        var obj = JsonSerializer.Deserialize<IEnumerable<AuthorsNames>>(Json, Essential.JsonSerializerOptions);

        var an = obj.First();

        if (an.Entity.Id != authorMock.Object.Id || !an.Language.Equals("default", StringComparison.Ordinal)
                    || !an.Text.Equals("name", StringComparison.Ordinal))
            Assert.Fail();
    }
}

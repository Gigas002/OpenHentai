using System.Globalization;
using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Descriptors;

namespace OpenHentai.Tests.JsonConverters;

public class DatabaseEntityCollectionJsonConverterTests
{
    private const string Json = "{\"id\":1,\"authors\":[1],\"creations\":[],\"tags\":[]}";

    [Test]
    public void SerializationTest()
    {
        const ulong id = 1;
        var authorMock = new Mock<Author>(id);
        var circleMock = new Mock<Circle>(id);

        circleMock.Object.Authors.Add(authorMock.Object);

        var ser = JsonSerializer.Serialize(circleMock.Object, Essential.JsonSerializerOptions);

        if (!ser.Equals(Json, StringComparison.Ordinal))
            Assert.Fail();
    }

    [Test]
    public void DeserializationTest()
    {
        const ulong id = 1;
        var authorMock = new Mock<Author>(id);
        var circleMock = new Mock<Circle>(id);

        circleMock.Object.Authors.Add(authorMock.Object);

        var obj = JsonSerializer.Deserialize<Circle>(Json, Essential.JsonSerializerOptions);

        if (obj.Id != circleMock.Object.Id || obj.Authors.First().Id != authorMock.Object.Id)
            Assert.Fail();
    }
}

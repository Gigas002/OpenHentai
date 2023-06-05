using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenHentai.JsonConverters;

public class DatabaseEntityJsonConverter<T> : JsonConverter<T> where T : IDatabaseEntity, new()
{
    /// <inheritdoc />
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var existsInDb = reader.TryGetUInt64(out var id);

        return existsInDb ? Essential.GetEntityById<T>(id) : default(T?);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Id);
    }
}

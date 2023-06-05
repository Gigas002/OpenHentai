using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenHentai.JsonConverters;

// TODO: removable at some point in the future
// see: https://github.com/dotnet/runtime/issues/54189

public class DatabaseEntityCollectionJsonConverter<T> : JsonConverter<HashSet<T>> where T : IDatabaseEntity, new()
{
    /// <inheritdoc />
    public override HashSet<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

        var set = new HashSet<T>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray) break;

            var existsInDb = reader.TryGetUInt64(out var id);

            var entry = existsInDb ? Essential.GetEntityById<T>(id) : default(T?);

            set.Add(entry);
        }

        return set;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, HashSet<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var entry in value)
        {
            if (entry is null)
                writer.WriteNullValue();
            else
                writer.WriteNumberValue(entry.Id);
        }

        writer.WriteEndArray();
    }
}

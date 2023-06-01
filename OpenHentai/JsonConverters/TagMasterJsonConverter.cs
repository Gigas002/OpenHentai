using System.Text.Json;
using System.Text.Json.Serialization;
using OpenHentai.Tags;

namespace OpenHentai.JsonConverters;

public class TagMasterJsonConverter : JsonConverter<Tag?>
{
    /// <inheritdoc />
    public override Tag? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var masterExists = reader.TryGetUInt64(out var id);

        return masterExists ? new Tag() { Id = id } : null;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Tag? value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Id);
    }
}

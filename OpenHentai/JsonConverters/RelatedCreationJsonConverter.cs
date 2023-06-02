using System.Text.Json;
using System.Text.Json.Serialization;
using OpenHentai.Creations;

namespace OpenHentai.JsonConverters;

public class RelatedCreationJsonConverter : JsonConverter<Creation>
{
    /// <inheritdoc />
    public override Creation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var masterExists = reader.TryGetUInt64(out var id);

        // TODO: this is wrong if creation is abstract
        return new Creation(id);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Creation value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Id);
    }
}

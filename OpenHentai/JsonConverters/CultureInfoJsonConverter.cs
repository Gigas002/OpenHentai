using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace OpenHentai.JsonConverters;

/// <inheritdoc />
public class CultureInfoJsonConverter : JsonConverter<CultureInfo>
{
    /// <inheritdoc />
    public override CultureInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var languageCode = reader.GetString();

        return new CultureInfo(languageCode);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

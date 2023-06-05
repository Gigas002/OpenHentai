using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OpenHentai.ValueConverters;

public class JsonValueConverter<T> : ValueConverter<T, string> where T : class
{
    public JsonValueConverter() : base(
            v => JsonSerializer.Serialize(v, Essential.JsonSerializerOptions),
            v => JsonSerializer.Deserialize<T>(v, Essential.JsonSerializerOptions))
    { }
}

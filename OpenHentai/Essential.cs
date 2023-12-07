using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenHentai;

public static class Essential
{
    public static JsonSerializerOptions JsonSerializerOptions => new()
    {
        // allows writing of cyrillic and other symbols as-is
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        // WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public static T GetEntityById<T>(ulong id) where T : IDatabaseEntity, new() => new() { Id = id };
}

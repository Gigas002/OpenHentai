using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenHentai.Helpers;

namespace OpenHentai;

public static class Essential
{
    // TODO: net8+ PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    public static JsonSerializerOptions JsonSerializerOptions => new()
    {
        // allows writing of cyrillic and other symbols as-is
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = new SnakeCaseLowerNamingPolicy(),
        // WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public static T GetEntityById<T>(ulong id) where T : IDatabaseEntity, new() => new() { Id = id };
}

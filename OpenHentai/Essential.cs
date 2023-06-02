using System.Text.Json;
using System.Text.Json.Serialization;
using OpenHentai.Helpers;

namespace OpenHentai;

public static class Essential
{
    // TODO: net8+ PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
    {
        PropertyNamingPolicy = new SnakeCaseLowerNamingPolicy(),
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };
}
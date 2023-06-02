using System.Text.Json;
using Humanizer;

namespace OpenHentai.Helpers;

// TODO: removable in net8+ together with Humanizer dependency
internal class SnakeCaseLowerNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.Underscore().ToLowerInvariant();
    }
}

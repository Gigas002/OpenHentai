using System.Text.Json;
using Humanizer;

#pragma warning disable CA1308

namespace OpenHentai.Helpers;

// TODO: removable in net8+ together with Humanizer dependency
internal class SnakeCaseLowerNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.Underscore().ToLowerInvariant();
}

using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Converters;

// TODO: Probably a better way will be introduced for handling json sql poperties in net8

public class ExternalLinkInfoConverter : ValueConverter<List<ExternalLinkInfo>, string>
{
    public ExternalLinkInfoConverter() : base(
        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
        v => JsonSerializer.Deserialize<List<ExternalLinkInfo>>(v, new JsonSerializerOptions())!)
    {
    }
}

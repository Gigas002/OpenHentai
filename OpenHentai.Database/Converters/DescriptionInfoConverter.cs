using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenHentai.Descriptors;

namespace OpenHentai.Database;

// TODO: Probably a better way will be introduced for handling json sql poperties in net8

public class DescriptionInfoConverter : ValueConverter<DescriptionInfo, string>
{
    public DescriptionInfoConverter() : base(
        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
        v => JsonSerializer.Deserialize<DescriptionInfo>(v, new JsonSerializerOptions())!)
    {
    }
}

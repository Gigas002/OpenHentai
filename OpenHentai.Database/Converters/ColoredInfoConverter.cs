using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Converters;

// TODO: Probably a better way will be introduced for handling json sql poperties in net8
// TODO: test if List is really a requirement

public class ColoredInfoConverter : ValueConverter<List<ColoredInfo>, string>
{
    public ColoredInfoConverter() : base(
        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
        v => JsonSerializer.Deserialize<List<ColoredInfo>>(v, new JsonSerializerOptions())!)
    {
    }
}

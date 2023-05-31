using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Descriptors;

namespace OpenHentai.Database;

[Table("media")]
public class Media : IDatabaseEntity, IMediaInfo
{
    public ulong Id { get; set; }
    public Uri Source { get; set; }
    public MediaType Type { get; set; }
}

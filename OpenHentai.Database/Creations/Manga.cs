using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Creations;

[Table("manga")]
public class Manga : Creation //, IManga
{
    public int Length { get; set; }
    public int Volumes { get; set; }
    public int Chapters { get; set; }
    public bool HasImages { get; set; }
    
    [Column(TypeName = "jsonb")]
    public IEnumerable<ColoredInfo> ColoredInfo { get; set; } = null!;
}

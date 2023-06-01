using OpenHentai.Creations;
using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Creations;

[Table("manga")]
public class Manga : Creation
{
    #region Properties
    
    public int Length { get; set; }

    public int Volumes { get; set; }
    
    public int Chapters { get; set; }
    
    public bool HasImages { get; set; }
    
    [Column(TypeName = "jsonb")]
    public HashSet<ColoredInfo> ColoredInfo { get; init; } = new();

    #endregion
}

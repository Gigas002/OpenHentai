using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Circles;

[Table("circles")]
public class Circle : IDatabaseEntity//: ICircle
{
    public ulong Id { get; set; }

    public List<CirclesTitles> Titles { get; set; } = new();

    public List<Author> Authors { get; set; } = new();

    public List<Creation> Creations { get; set; } = new();
}

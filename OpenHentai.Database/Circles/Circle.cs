using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Circles;

[Table("circles")]
public class Circle : IDatabaseEntity//: ICircle
{
    /// <inheritdoc />
    public ulong Id { get; set; }

    public IEnumerable<CirclesTitles> Titles { get; set; } = null!;

    public IEnumerable<Author> Authors { get; set; } = null!;

    public IEnumerable<Creation>? Creations { get; set; }
}

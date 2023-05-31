using OpenHentai.Descriptors;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Circles;

[Table("circles")]
public class Circle : IDatabaseEntity//: ICircle
{
    public ulong Id { get; set; }

    [NotMapped]
    public IEnumerable<LanguageSpecificTextInfo> Titles { get; set; }

    public IEnumerable<Author> Authors { get; set; }

    public IEnumerable<Creation> Creations { get; set; }
}

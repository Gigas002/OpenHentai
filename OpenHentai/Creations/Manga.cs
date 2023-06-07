using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Constants;
using OpenHentai.Descriptors;

namespace OpenHentai.Creations;

[Table(TableNames.Manga)]
public class Manga : Book
{
    public Manga() : base() { }

    public Manga(ulong id) : base(id) { }

    public Manga(LanguageSpecificTextInfo title) : base(title) { }

    public Manga(string formattedTitle) : base(formattedTitle) { }
}

using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Constants;
using OpenHentai.Descriptors;

namespace OpenHentai.Creations;

[Table(TableNames.Manga)]
public class Manga : Book
{
    public Manga() : base() { HasImages = true; }

    public Manga(ulong id) : base(id) { HasImages = true; }

    public Manga(LanguageSpecificTextInfo title) : base(title) { HasImages = true; }

    public Manga(string formattedTitle) : base(formattedTitle) { HasImages = true; }
}

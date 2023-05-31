using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace OpenHentai.Database.Relative;

[Table("creatures_names")]
public class CreaturesNames : IDatabaseEntity
{
    public ulong Id { get; set; }

    public Creature Creature { get; set; }

    public string Name { get; set; }

    public string Language { get; set; }

    public CreaturesNames() { }

    public CreaturesNames(string name, string language) => (Name, Language) = (name, language);

    public CreaturesNames(string name, CultureInfo language) : this(name, language.ToString()) { }

    public CreaturesNames(LanguageSpecificTextInfo name) : this(name.Text, name.Language) { }

    public LanguageSpecificTextInfo GetNameInfo()
    {
        var ci = new CultureInfo(Language);

        return new LanguageSpecificTextInfo(ci, Name);
    }
}

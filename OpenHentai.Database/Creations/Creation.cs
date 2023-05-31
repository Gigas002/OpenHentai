using OpenHentai.Database.Circles;
using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Creations;

[Table("creations")]
public class Creation : IDatabaseEntity //: ICreation
{
    public ulong Id { get; set; }

    [NotMapped]
    public IEnumerable<LanguageSpecificTextInfo> Titles { get; set; }
    
    public List<AuthorsCreations> Authors { get; init; } = new();

    public IEnumerable<Circle> Circles { get; set; }

    public DateTime? PublishStarted { get; set; }

    public DateTime? PublishEnded { get; set; }

    [NotMapped]
    public IEnumerable<ExternalLinkInfo> Sources { get; set; }

    [NotMapped]
    public DescriptionInfo Description { get; set; }
    
    [NotMapped]
    public IDictionary<Creation, CreationRelations> Relations { get; init; }
    
    // [NotMapped]
    // public IEnumerable<ICreationCollection> Collections { get; set; }
    
    public List<CreationsCharacters> Characters { get; init; } = new();

    [NotMapped]
    public IEnumerable<PictureInfo> Pictures { get; set; }
    
    [NotMapped]
    public IEnumerable<LanguageInfo> Languages { get; set; }

    public OpenHentai.Creations.Rating Rating { get; set; }

    public PublishStatus Status { get; set; }

    [NotMapped]
    public IEnumerable<CensorshipInfo> Censorship { get; set; }

    public List<Tag> Tags { get; set; } = new();
}
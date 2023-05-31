using OpenHentai.Database.Circles;
using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Statuses;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Creations;

namespace OpenHentai.Database.Creations;

[Table("creations")]
public class Creation : IDatabaseEntity //: ICreation
{
    public ulong Id { get; set; }

    public List<CreationsTitles> Titles { get; set; } = new();
    
    public List<AuthorsCreations> Authors { get; init; } = new();

    // TODO: see ~CreationsCircles.cs
    public List<Circle> Circles { get; set; } = new();

    public DateTime? PublishStarted { get; set; }

    public DateTime? PublishEnded { get; set; }

    [Column(TypeName = "jsonb")]
    public List<ExternalLinkInfo> Sources { get; set; } = new();

    [Column(TypeName = "jsonb")]
    public DescriptionInfo Description { get; set; }
    
    public List<CreationsRelations> Relations { get; init; } = new();
    
    // [NotMapped]
    // public IEnumerable<ICreationCollection> Collections { get; set; }
    
    public List<CreationsCharacters> Characters { get; init; } = new();

    public List<MediaInfo> Media { get; set; } = new();
    
    [Column(TypeName = "jsonb")]
    public List<LanguageInfo> Languages { get; set; } = new();

    public Rating Rating { get; set; }

    public PublishStatus Status { get; set; }

    [Column(TypeName = "jsonb")]
    public List<CensorshipInfo> Censorship { get; set; } = new();

    public List<Tag> Tags { get; set; } = new();
}

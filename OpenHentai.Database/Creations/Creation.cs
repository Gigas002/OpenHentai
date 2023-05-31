using OpenHentai.Database.Circles;
using OpenHentai.Descriptors;
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

    public IEnumerable<CreationsTitles> Titles { get; set; } = null!;
    
    public IEnumerable<AuthorsCreations> Authors { get; set; } = null!;

    // TODO: see ~CreationsCircles.cs
    public IEnumerable<Circle>? Circles { get; set; }

    public DateTime? PublishStarted { get; set; }

    public DateTime? PublishEnded { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<ExternalLinkInfo>? Sources { get; set; }

    // TODO: consider rework
    [Column(TypeName = "jsonb")]
    public DescriptionInfo? Description { get; set; }
    
    public IEnumerable<CreationsRelations>? Relations { get; set; }
    
    // [NotMapped]
    // public IEnumerable<ICreationCollection> Collections { get; set; }
    
    public IEnumerable<CreationsCharacters>? Characters { get; set; }

    public IEnumerable<MediaInfo>? Media { get; set; }
    
    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageInfo> Languages { get; set; } = null!;

    public Rating Rating { get; set; }

    public PublishStatus Status { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<CensorshipInfo>? Censorship { get; set; }

    public IEnumerable<Tag> Tags { get; set; } = null!;
}

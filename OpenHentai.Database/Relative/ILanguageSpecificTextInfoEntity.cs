using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Database.Relative;

public interface ILanguageSpecificTextInfoEntity<T> : IDatabaseEntity where T : class
{
    public T Entity { get; set; }

    public string Text { get; set; }

    public string? Language { get; set; }

    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo();
}


// TODO: consider implementing this way

// public interface IManyToMany<TOrigin,TRelated,TEnum> where TOrigin : class
//                                                      where TRelated : class
//                                                      where TEnum : System.Enum
// {
//     public TOrigin OriginEntity { get; set; }

//     public TRelated RelatedEntity { get; set; }

//     public TEnum Relation { get; set; }
// }

// [Table("authors_creations")]
// [PrimaryKey("author_id", "creation_id")]
// public class ACreations : IManyToMany<Author, Creation, AuthorRole>
// {
//     [ForeignKey("author_id")]
//     public Author OriginEntity { get; set; }
    
//     [ForeignKey("creation_id")]
//     public Creation RelatedEntity { get; set; }

//     [Column("role")]
//     public AuthorRole Relation { get; set; }
// }

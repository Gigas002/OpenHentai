using OpenHentai.Descriptors;

namespace OpenHentai.Database.Relative;

public interface ILanguageSpecificTextInfoEntity<T> : IDatabaseEntity where T : class
{
    #region Properties

    public T Entity { get; set; }

    public string Text { get; set; }

    public string? Language { get; set; }

    #endregion

    #region Methods
    
    public LanguageSpecificTextInfo GetLanguageSpecificTextInfo();

    #endregion
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

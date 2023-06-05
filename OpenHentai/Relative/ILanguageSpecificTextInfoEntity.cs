using OpenHentai.Descriptors;

namespace OpenHentai.Relative;

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

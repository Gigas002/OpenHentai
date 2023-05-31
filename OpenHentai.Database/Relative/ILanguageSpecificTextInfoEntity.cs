namespace OpenHentai.Database.Relative;

public interface ILanguageSpecificTextInfoEntity<T> : IDatabaseEntity where T : class
{
    public T Entity { get; set; }

    public string Text { get; set; }

    public string Language { get; set; }
}

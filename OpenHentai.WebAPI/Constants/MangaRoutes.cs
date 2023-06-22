namespace OpenHentai.WebAPI.Constants;

public static class MangaRoutes
{
    public const string Base = "/manga";

    public const string Id = "{id}";

    public const string Titles = $"{Id}/titles";

    public const string Authors = $"{Id}/authors";

    public const string Circles = $"{Id}/circles";

    public const string Relations = $"{Id}/relations";

    public const string Characters = $"{Id}/characters";

    public const string Tags = $"{Id}/tags";
}

namespace OpenHentai.WebAPI.Constants;

public static class CirclesRoutes
{
    public const string Base = "/circles";

    public const string Id = "{id}";

    public const string AllTitles = "titles";

    public const string Titles = $"{Id}/titles";

    public const string Authors = $"{Id}/authors";

    public const string Creations = $"{Id}/creations";

    public const string Tags = $"{Id}/tags";
}

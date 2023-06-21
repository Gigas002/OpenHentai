namespace OpenHentai.WebAPI.Constants;

public static class AuthorsRoutes
{
    public const string Base = "/authors";

    public const string Id = "{id}";

    public const string AuthorsNames = "authors_names";

    public const string AuthorNames = $"{Id}/author_names";

    public const string Circles = $"{Id}/circles";

    public const string Creations = $"{Id}/creations";

    public const string Names = $"{Id}/names";

    public const string Tags = $"{Id}/tags";

    public const string Relations = $"{Id}/relations";
}

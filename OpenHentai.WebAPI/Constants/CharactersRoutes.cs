namespace OpenHentai.WebAPI.Constants;

public static class CharactersRoutes
{
    public const string Base = "/characters";

    public const string Id = "{id}";

    public const string Creations = $"{Id}/creations";

    public const string Names = $"{Id}/names";

    public const string Tags = $"{Id}/tags";

    public const string Relations = $"{Id}/relations";
}

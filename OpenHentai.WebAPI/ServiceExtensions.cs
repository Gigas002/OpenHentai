using OpenHentai.Contexts;

namespace OpenHentai.WebAPI;

public static class ServiceExtensions
{
    public static void ConfigureContextHelpers(this IServiceCollection services)
    {
        services.AddScoped<AuthorsContextHelper>();
        services.AddScoped<CharactersContextHelper>();
        services.AddScoped<CirclesContextHelper>();
        services.AddScoped<MangaContextHelper>();
        services.AddScoped<TagsContextHelper>();
    }
}

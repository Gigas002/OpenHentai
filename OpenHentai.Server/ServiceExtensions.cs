using OpenHentai.Repositories;

namespace OpenHentai.Server;

public static class ServiceExtensions
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<ICharactersRepository, CharactersRepository>();
        services.AddScoped<ICirclesRepository, CirclesRepository>();
        services.AddScoped<IMangaRepository, MangaRepository>();
        services.AddScoped<ITagsRepository, TagsRepository>();
    }
}

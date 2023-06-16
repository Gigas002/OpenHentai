using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using OpenHentai.JsonConverters;

namespace OpenHentai.WebAPI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel((_, options) =>
        {
            options.ListenAnyIP(5230, listenOptions =>
            {
                // for http1.1 and http2 support set to: Http1AndHttp2AndHttp3
                // listenOptions.Protocols = HttpProtocols.Http3;
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                listenOptions.UseHttps();
            });
        });
        
        builder.Services.AddAntiforgery();

        builder.Services.AddDbContext<DatabaseContext>();

        // for controllers-based approach
        builder.Services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });
        // }).AddJsonOptions(options =>
        // {
        //     options.JsonSerializerOptions.Converters.Add(new CultureInfoJsonConverter());
        // });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API - V1",
                Version = "v1",
                Description = "A sample API to demo Swashbuckle",
                Contact = new OpenApiContact
                {
                    Name = "gigas002",
                    Email = "test@test.test"
                },
                License = new OpenApiLicense
                {
                    Name = "AGPL-3.0-only",
                    Url = new Uri("https://www.gnu.org/licenses/agpl-3.0.txt")
                }
            });

            // uses reflection
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{nameof(OpenHentai)}.{nameof(WebAPI)}.xml");
            options.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // for swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        // for redoc, path /api-docs by default
        app.UseReDoc();

        app.UseHttpsRedirection();

        // for controllers-based approach
        app.MapControllers();

        await app.RunAsync().ConfigureAwait(false);
    }
}

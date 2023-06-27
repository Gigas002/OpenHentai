using System.Globalization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace OpenHentai.WebAPI;

public static class Program
{
    public const string DatabasePath = "../openhentai.db";

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // create a file logger using Serilog
        var logger = new LoggerConfiguration()
            .WriteTo.File("../httplogs.txt")
            .WriteTo.Console()
            .CreateLogger();

        // add the file logger to the LoggerFactory
        builder.Logging.AddSerilog(logger);

        // configure the HttpLoggingOptions
        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
        });

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

        builder.Services.AddDbContext<DatabaseContext>(options => 
        {
            options.UseSqlite($"Data Source={DatabasePath}");
        });

        // configure controllers's context helpers
        builder.Services.ConfigureRepositories();

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
            options.MapType<CultureInfo>(() => new OpenApiSchema() { Type = "string" });
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

        app.UseHttpLogging();

        await app.RunAsync().ConfigureAwait(false);
    }
}
